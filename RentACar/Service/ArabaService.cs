
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class ArabaService :IAraba
{
    private readonly RentACarDbContext _dbContext;

    public ArabaService(RentACarDbContext context)
    {
        _dbContext = context;
    }
    
    
   public async Task<PagedResponse<ArabaResponseDto>> GetAllAraba(ArabaFilterDto filter, int page = 1, int pageSize = 10)
    {
        var query = _dbContext.Arabalarr
            .Include(x=>x.CarModeli)
            .ThenInclude(m=>m.Marka)
            .Include(x=>x.Vites)
            .Include(x=>x.Yakit)
            .AsQueryable();
    
        
        if (filter.ModelId.HasValue)
        {
           query= query.Where(x=>x.CarModeliID==filter.ModelId.Value);
        }
        
        if (filter.MarkaId.HasValue)
        {
            query=query.Where(x=>x.CarModeli.MarkaId==filter.MarkaId.Value);
        }

        if (filter.VitesId.HasValue)
        {
            query=query.Where(x=>x.VitesID==filter.VitesId.Value);
        }

        if (filter.YakitId.HasValue)
        {
            query=query.Where(x=>x.YakitID==filter.YakitId.Value);
        }

        if (filter.MaxFiyat.HasValue)
        {
            query=query.Where(x=>x.ArabaFiyat<=filter.MaxFiyat.Value);
        }

        if (filter.MinFiyat.HasValue)
        {
            query=query.Where(x=>x.ArabaFiyat>=filter.MinFiyat.Value);
        }

        if (filter.MaxKm.HasValue)
        {
            query=query.Where(x=>x.ToplamKm<=filter.MaxKm.Value);
        }

        if (filter.MinKm.HasValue)
        {
            query=query.Where(x=>x.ToplamKm>=filter.MinKm.Value);
        }

        if (filter.MaxYil.HasValue)
        {
            query=query.Where(x=>x.ArabaYasi<=filter.MaxYil.Value);
        }

        if (filter.MinYil.HasValue)
        {
            query=query.Where(x=>x.ArabaYasi>=filter.MinYil.Value);
        }

        query = query.OrderBy(x => x.Id);

        var toplamSayi = await query.CountAsync();

        var arabalar = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<ArabaResponseDto>()
        {
            TotalCount = toplamSayi,
            Page = page,
            PageSize = pageSize,
            Items = arabalar.Select(x=>x.ToArabaDto()),
        };
    }
    
    public async Task<ArabaResponseDto> GetArabaById(int arabaId)
    {
       var araba = await _dbContext.Arabalarr
           .Include(x=>x.CarModeli)
           .ThenInclude(m=>m.Marka)
           .Include(x=>x.Vites)
           .Include(x=>x.Yakit)
           .FirstOrDefaultAsync(x=>x.Id == arabaId);
       return araba?.ToArabaDto();
            
    }

    public async Task<ArabaResponseDto> CreateAraba(ArabaCreateDto dto)
    {
      var modelVarmi=await _dbContext.CarModelleri.AnyAsync(x=>x.Id==dto.ModelId);
      if (modelVarmi==false)
      {
          return null;
      }
      
      var vitesVarmi =await  _dbContext.Vitesler.AnyAsync(x=>x.VitesId==dto.VitesId);
      if (vitesVarmi == false)
      {
          return null;
          
      }
      var yakitVarmi=await _dbContext.Yakitlar.AnyAsync(x=>x.YakitId==dto.YakitId);
      if (yakitVarmi == false)
      {
          return null;
      }

      var araba = new Araba
      {
          ArabaAdi = dto.ArabaAdi,
          ArabaYasi = dto.ArabaYasi,
          ArabaFiyat = dto.ArabaFiyat,
          ToplamKm = dto.ToplamKm,
          CarModeliID = dto.ModelId,
          VitesID = dto.VitesId,
          YakitID = dto.YakitId,
      };

      _dbContext.Arabalarr.Add(araba);
      await _dbContext.SaveChangesAsync();
      
      await _dbContext.Entry(araba).Reference(x => x.CarModeli).LoadAsync();
      await _dbContext.Entry(araba.CarModeli).Reference(x => x.Marka).LoadAsync();
      await _dbContext.Entry(araba).Reference(x => x.Vites).LoadAsync();
     await  _dbContext.Entry(araba).Reference(x => x.Yakit).LoadAsync();
      
      return araba.ToArabaDto();
    }

    public async Task<bool> UpdateAraba(int id, ArabaCreateDto dto)
    {
        var araba=await _dbContext.Arabalarr
            .FirstOrDefaultAsync(x=>x.Id == id);
        
        if (araba == null)
        {
            return false;
        }
        araba.ArabaAdi = dto.ArabaAdi;
        araba.ArabaYasi = dto.ArabaYasi;
        araba.ArabaFiyat = dto.ArabaFiyat;
        araba.ToplamKm = dto.ToplamKm;
        araba.CarModeliID = dto.ModelId;
        araba.VitesID = dto.VitesId;
        araba.YakitID = dto.YakitId;
        
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAraba(int id)
    {
        var araba = await  _dbContext.Arabalarr
            .FirstOrDefaultAsync(x => x.Id == id);
        if (araba == null)
        {
            return false;
        }
    _dbContext.Arabalarr.Remove(araba);
   await  _dbContext.SaveChangesAsync();
    return true;
    }
}