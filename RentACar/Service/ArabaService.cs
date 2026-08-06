using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class ArabaService :IAraba
{
    private readonly RentACarDbContext _dbContext;

    public ArabaService(RentACarDbContext _context)
    {
        _dbContext = _context;
    }
    
    
   public IEnumerable<ArabaResponseDto> GetAllAraba(ArabaFilterDto filter)
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

        return query.ToList().Select(x => x.ToArabaDto());
    }
    
    public ArabaResponseDto GetArabaById(int arabaId)
    {
       var araba = _dbContext.Arabalarr
           .Include(x=>x.CarModeli)
           .ThenInclude(m=>m.Marka)
           .Include(x=>x.Vites)
           .Include(x=>x.Yakit)
           .FirstOrDefault(x=>x.Id == arabaId);
       return araba?.ToArabaDto();
            
    }

    public ArabaResponseDto CreateAraba(ArabaCreateDto dto)
    {
      var modelVarmi=_dbContext.CarModelleri.Any(x=>x.Id==dto.ModelId);
      if (modelVarmi==false)
      {
          return null;
      }
      
      var vitesVarmi = _dbContext.Vitesler.Any(x=>x.VitesId==dto.VitesId);
      if (vitesVarmi == false)
      {
          return null;
          
      }
      var YakitVarmi=_dbContext.Yakitlar.Any(x=>x.YakitId==dto.YakitId);
      if (YakitVarmi == false)
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
      _dbContext.SaveChanges();
      
      _dbContext.Entry(araba).Reference(x => x.CarModeli).Load();
      _dbContext.Entry(araba.CarModeli).Reference(x => x.Marka).Load();
      _dbContext.Entry(araba).Reference(x => x.Vites).Load();
      _dbContext.Entry(araba).Reference(x => x.Yakit).Load();
      
      return araba.ToArabaDto();
    }

    public bool UpdateAraba(int id, ArabaCreateDto dto)
    {
        var araba=_dbContext.Arabalarr
            .FirstOrDefault(x=>x.Id == id);
        
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
        
        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteAraba(int id)
    {
        var araba = _dbContext.Arabalarr
            .FirstOrDefault(x => x.Id == id);
        if (araba == null)
        {
            return false;
        }
    _dbContext.Arabalarr.Remove(araba);
    _dbContext.SaveChanges();
    return true;
    }
}