using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Exceptions;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class RentalService : IRental
{

    private readonly RentACarDbContext _context;

    public RentalService(RentACarDbContext context)
    {
        _context = context;
    }

    public async Task<RentalResponseDto> CreateRental(RentalCreateDto rentalCreateDto,int userId)
    {
        var araba= await _context.Arabalarr.
            FirstOrDefaultAsync(x => x.Id ==rentalCreateDto.ArabaId);
        if (araba == null)
        {
            throw new NotFoundException("Araba mevcut degil");
        }

        if (rentalCreateDto.StartDate >= rentalCreateDto.EndDate || rentalCreateDto.StartDate < DateTime.Now)
        {
            throw new ValidationException("Gecerli Tarih Girin ");
        }
        var cakisanRentalVarMi = await _context.Rentals.AnyAsync(x =>
            x.ArabaId == rentalCreateDto.ArabaId &&
            x.Status == RentalStatus.Acik &&
            x.StartDate < rentalCreateDto.EndDate &&
            x.EndDate > rentalCreateDto.StartDate);

        if (cakisanRentalVarMi)
        {
            throw new ConflictException("Araç bu tarihlerde müsait değil");
        }

        var gunSayisi = (rentalCreateDto.EndDate - rentalCreateDto.StartDate).Days;
        var totalPrice = araba.ArabaFiyat * gunSayisi;

        var rental = new Rental()
        {
            ArabaId = rentalCreateDto.ArabaId,
            UserId = userId,
            StartDate = rentalCreateDto.StartDate,
            EndDate = rentalCreateDto.EndDate,
            TotalPrice = totalPrice,
            DailyPrice = araba.ArabaFiyat,

        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        return rental.ToRentalDto();
    }

    public async Task<PagedResponse<RentalResponseDto>> GetMyRentals(int userId, int page = 1, int pageSize = 10)
    {
        var query = _context.Rentals
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.StartDate);

        var toplamSayi = await query.CountAsync();

        var rentals = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<RentalResponseDto>()
        {
            TotalCount = toplamSayi,
            Page = page,
            PageSize = pageSize,
            Items = rentals.Select(x => x.ToRentalDto()),
        };
    }

    public async Task<RentalResponseDto> GetRentalById(int id, int userId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == id);

        if (rental == null)
        {
            throw new NotFoundException("Kiralama bulunamadı");
        }

        if (rental.UserId != userId)
        {
            throw new UnauthorizedAccessException("Bu kiralamaya erişim yetkiniz yok");
        }

        return rental.ToRentalDto();
    }

    public async Task<RentalResponseDto> CancelRental(int id, int userId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == id);

        if (rental == null)
        {
            throw new NotFoundException("Kiralama bulunamadı");
        }

        if (rental.UserId != userId)
        {
            throw new UnauthorizedAccessException("Bu kiralamayı iptal etme yetkiniz yok");
        }

        if (rental.Status != RentalStatus.Acik)
        {
            throw new ConflictException("Bu kiralama zaten iptal edilmiş veya tamamlanmış");
        }

        if (rental.StartDate <= DateTime.Now)
        {
            throw new ConflictException("Başlamış bir kiralama iptal edilemez");
        }

        rental.Status = RentalStatus.IptalEdildi;
        await _context.SaveChangesAsync();

        return rental.ToRentalDto();
    }

    public async Task<PagedResponse<RentalResponseDto>> GetAllRentals(int page = 1, int pageSize = 10)
    {
        var query = _context.Rentals
            .OrderByDescending(x => x.StartDate);

        var toplamSayi = await query.CountAsync();

        var rentals = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<RentalResponseDto>()
        {
            TotalCount = toplamSayi,
            Page = page,
            PageSize = pageSize,
            Items = rentals.Select(x => x.ToRentalDto()),
        };
    }
}

