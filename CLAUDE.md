# CLAUDE.md

Bu dosya, bu repoda çalışan Claude Code için proje rehberidir.

## Proje

RentACar — ASP.NET Core Web API (net10.0), EF Core + SQL Server, JWT tabanlı kimlik doğrulama.
Tek proje: `RentACar/` (solution: `RentACar.sln`).

Domain: `Marka` → `CarModeli` → `Araba` (+ `Vites`, `Yakit` lookup tabloları), `User`, `Rental`.
Kod ve isimlendirme Türkçe; entity/property/metot adlarında bu dili koru (`GetAllAraba`, `cakisanRentalVarMi` gibi).

## Komutlar

```powershell
dotnet build
dotnet run --project RentACar            # https://localhost:.../swagger (yalnızca Development)
dotnet ef migrations add <Ad> --project RentACar
dotnet ef database update --project RentACar
```

`Jwt:SecretKey` ve gerçek connection string user-secrets'ta tutulur (`UserSecretsId` csproj'da tanımlı); `appsettings.json`'a secret yazma.

## Katman mimarisi

Akış her zaman: **Controller → Interface → Service → DbContext**, dönüşümler **Mapper**, taşınan veri **DTO**.

- **`Controller/`** — HTTP katmanı. Sadece: yetkilendirme attribute'ları, JWT'den `userId` okuma, servisi çağırma, sonucu HTTP durumuna çevirme (`Ok`, `NotFound`, `BadRequest`, `Unauthorized`). İş kuralı, EF sorgusu veya `DbContext` erişimi **controller'a girmez**. Bağımlılık daima interface üzerinden ctor injection ile alınır (`IAraba`, `IRental`…), somut servis tipi asla enjekte edilmez.
- **`Service/`** — İş mantığının tamamı: doğrulama, ilişki varlık kontrolleri, fiyat/çakışma hesapları, EF sorguları, `SaveChangesAsync`. Servisler DTO alır ve DTO/`bool` döner; entity dışarı sızmaz.
- **`Service/I*.cs`** — Her servisin interface'i, servisle **aynı klasörde** durur (`IAraba.cs` + `ArabaService.cs`). Yeni servis eklerken interface'i de burada oluştur ve `Program.cs`'te `AddScoped<IX, XService>()` ile kaydet.
- **`DTOs/`** — Sınır tipleri. Üç desen var: `XCreateDto` (girdi), `XResponseDto` (çıktı), `XFilterDto` (nullable query parametreleri, `[FromQuery]`). Entity'ler asla request/response gövdesi olarak kullanılmaz.
- **`Mappers/`** — `static class XMapper` içinde extension metot (`this Araba araba` → `ToArabaDto()`). Entity→DTO dönüşümü sadece burada yapılır; servis içinde elle DTO kurma. (İstisna: `UserService.RegisterAsync` hâlâ elle map ediyor — yeni kod mapper kullanır.)
- **`Exceptions/`** — Custom exception tipleri (`NotFoundException`, `ValidationException`, `ConflictException`) ve `GlobalExceptionHandler`. Yeni bir hata sınıfı gerekirse buraya eklenir ve handler'daki `switch` ifadesine bir satır olarak işlenir.
- **`Model/`** — EF entity'leri. Navigation property + `[ForeignKey(nameof(X))]` ile FK ikilisi şeklinde yazılır.
- **`Data/RentACarDbContext.cs`** — `DbSet`'ler. Yeni entity eklerken buraya `DbSet` ekle ve migration üret.

## Hata yönetimi — tutarsızlık, bilinçli olarak henüz standartlaştırılmadı

Projede **iki desen birlikte** yaşıyor:

- **Null/bool dönen servisler** (`ArabaService`, `MarkaService`, `ModelService`, `VitesService`, `YakitService`, `UserService`): geçersiz durumda `return null` / `return false`. Controller bu sonucu HTTP'ye çevirir (`if (sonuc == null) return BadRequest(...)`).
- **Exception fırlatan servisler** (`RentalService`): custom exception fırlatır, `GlobalExceptionHandler` bunu HTTP'ye çevirir. Controller `try/catch` **yazmaz**, sadece servisi çağırıp `Ok(...)` döner.

**Kural: mevcut entity'ye dokunuyorsan o entity'nin desenini sürdür.** `ArabaService`'e metot eklerken null dön, `RentalService`'e eklerken exception fırlat. Tek seferde tüm projeyi tek desene geçirmeye kalkma — bu ayrı ve açık bir karar olarak istenmeli.

### Merkezî exception handling

`Program.cs`'te `AddExceptionHandler<GlobalExceptionHandler>()` + `AddProblemDetails()` kayıtlı, pipeline'ın en başında `app.UseExceptionHandler()` çağrılıyor. Handler `ProblemDetails` gövdesiyle yanıt üretir.

| Exception | HTTP | Ne zaman |
|---|---|---|
| `NotFoundException` | 404 | Kayıt yok |
| `ValidationException` | 400 | Girdi geçersiz (tarih, alan değeri) |
| `ConflictException` | 409 | Kayıt var ama işlem mevcut durumla çelişiyor (dolu tarih, zaten iptal, başlamış kiralama) |
| `UnauthorizedAccessException` | 403 | Sahiplik kontrolü başarısız (IDOR) |
| diğer her şey | 500 | Beklenmeyen; loglanır, mesaj dışarı sızmaz |

- Exception fırlatan servislerde **düz `throw new Exception(...)` yazma** — üç custom tipten uygun olanı seç.
- Sahiplik ihlalinde `UnauthorizedAccessException` kullan; `ValidationException` değil (403 ile 400 karışır).
- 500'ün `Detail`'ı bilinçli olarak jenerik; hata ayrıntısı yalnızca log'a gider.

## Pagination

Liste dönen uçlar `PagedResponse<T>` (`DTOs/PagedResponse.cs`) döner: `TotalCount`, `Page`, `PageSize`, `Items`.

- Servis imzası: `(..., int page = 1, int pageSize = 10)`, dönüş `Task<PagedResponse<T>>`.
- Servis içinde sıra: **sırala → `CountAsync` → `Skip((page - 1) * pageSize).Take(pageSize)`**. `CountAsync` sayfalamadan *önce* çağrılır, yoksa toplam sayı yanlış çıkar.
- Sıralama `Skip`/`Take`'ten önce şart; sırasız sayfalama SQL Server'da tutarsız sonuç verir. Rental'da `OrderByDescending(x => x.StartDate)`.
- Controller tarafı: `[FromQuery] int page = 1, [FromQuery] int pageSize = 10`.
- Sayfalanan uçlar: `RentalService.GetMyRentals` / `GetAllRentals` (`OrderByDescending(x => x.StartDate)`) ve `ArabaService.GetAllAraba` (`OrderBy(x => x.Id)`). `ArabaService.GetAllAraba`'da filtreler önce uygulanır, sayfalama en sonda gelir — `TotalCount` filtrelenmiş sonucun sayısıdır.
- Henüz sayfalanmayan liste uçları: `GetAllMarka`, `GetAllModel`, `GetAllVitesAsync`, `GetAllYakit`. Bunlar küçük lookup tabloları; sayfalama istenirse aynı desen uygulanır.

## Sorgu seçimi

- Sadece **"var mı yok mu"** kontrolü yapıyorsan → `AnyAsync`. Örn. `CreateAraba`'daki `modelVarmi`/`vitesVarmi`/`yakitVarmi`, `CreateRental`'daki `cakisanRentalVarMi`.
- Kaydın **kendisine de ihtiyacın varsa** (alan okuyacak, güncelleyecek, silecek, DTO'ya çevireceksen) → `FirstOrDefaultAsync` ve null kontrolü. Örn. fiyatı okunan `araba`, güncellenen/silinen kayıtlar.
- Varlığı `AnyAsync` ile doğrulayıp hemen ardından aynı kaydı tekrar çekme; tek `FirstOrDefaultAsync` yeterlidir.
- Response DTO ilişkili alan içeriyorsa `Include`/`ThenInclude` şart (bkz. `ArabaMapper` `CarModeli.Marka.MarkaAdi` okur → `Include(x => x.CarModeli).ThenInclude(m => m.Marka)`).

## Guard clause

Hata/erken çıkış durumları önce ele alınır, ardından mutlu yol girintisiz devam eder. `else` bloğu kullanma, mutlu yolu `if` içine gömme.

```csharp
var araba = await _context.Arabalarr.FirstOrDefaultAsync(x => x.Id == dto.ArabaId);
if (araba == null)
{
    throw new NotFoundException("Araba mevcut degil");
}

// mutlu yol buradan itibaren, girintisiz
```

Filtreleme de aynı biçimde ardışık bağımsız `if`'lerle yazılır (`GetAllAraba`), zincirlenmiş `else if` ile değil.

## Yetkilendirme

- Admin işlemleri (create/update/delete, lookup yönetimi) → `[Authorize(Roles = "Admin")]`.
- Giriş yapmış herkese açık işlemler (kiralama oluşturma, kendi kayıtlarını görme) → düz `[Authorize]`.
- Kimlik doğrulaması istemeyen uçlar (`register`, `login`) attribute almaz.
- **`UserId` her zaman JWT'den okunur**, asla DTO'dan alınmaz:
  ```csharp
  var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
  ```
  ve servise ayrı parametre olarak geçilir (`CreateRental(dto, userId)`). `RentalCreateDto`'da bilinçli olarak `UserId` alanı yoktur — request'ten kullanıcı kimliği kabul eden bir DTO alanı ekleme.
- Token claim'leri `UserService.TokenUret` içinde üretilir: `NameIdentifier`, `Email`, `Role`. Rol varsayılanı `"Musteri"`.

## Sahiplik kontrolü (IDOR koruması)

Bir kaydı id ile çekip döndüren, güncelleyen veya silen **her yerde**, kaydın sahibi ile isteği atan kullanıcının eşleştiği doğrulanmalı. `[Authorize]` tek başına yeterli değildir — giriş yapmış herhangi bir kullanıcı başkasının `rentalId`'sini deneyebilir.

```csharp
var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentalId);
if (rental == null)
{
    throw new NotFoundException("Kiralama bulunamadı");
}
if (rental.UserId != userId)
{
    throw new UnauthorizedAccessException("Bu kayda erişim yetkiniz yok");
}
```

- Sahiplik kontrolü **servis katmanında** yapılır; controller yalnızca `userId`'yi taşır.
- Listeleme uçlarında da geçerli: kullanıcıya ait kayıtlar `Where(x => x.UserId == userId)` ile filtrelenir, tüm tablo dönülmez.
- Admin'in başkasının kaydına erişmesi gerekiyorsa bu, `[Authorize(Roles = "Admin")]` ile korunan **ayrı bir uç** olmalı; aynı uca rol kontrolüyle by-pass eklenmemeli.
- Sahiplik kavramı olan entity'ler: `Rental` (`UserId`). `Araba`/`Marka`/`Model`/`Vites`/`Yakit` katalog verisidir, sahiplik yerine rol kontrolüne tabidir.

## Bilinen açık noktalar

Bunlar mevcut durumdur; bilerek dokunmadıkça düzeltme yapma, ama yeni kodda tekrarlama:

- `ArabaController.GetArabaById` null geldiğinde `NotFound` yerine `Ok(null)` dönüyor.
- `UpdateAraba`/`DeleteAraba` gibi bazı uçlar `ActionResult` yerine düz `bool` dönüyor.
- `ModelService.UpdateModel` `MarkaId`'yi güncellemiyor.
- CORS politikası `GelistirmeIcin` her origin'e açık — geliştirme içindir.
- `Rental.CreateAt` `DateTime.Now`, JWT süresi `DateTime.UtcNow` bazlı; tarih karşılaştırmalarında bu farkı dikkate al.
