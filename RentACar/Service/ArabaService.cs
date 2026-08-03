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
    
    
    public IEnumerable<ArabaResponseDto> GetAllAraba()
    {
        return _dbContext.Arabalarr
            .Include(x=>x.CarModeli)
            .ThenInclude(m=>m.Marka)
            .Include(x=>x.Vites)
            .Include(x=>x.Yakit)
            .ToList()
            .Select(x=>x.ToArabaDto())
            .ToList();
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
          Id = dto.ModelId,
          VitesID = dto.VitesId,
          YakitID = dto.YakitId,
      };

      _dbContext.Arabalarr.Add(araba);
      _dbContext.SaveChanges();
      
      _dbContext.Entry(araba).Reference(x => x.CarModeli).Load();
      _dbContext.Entry(araba).Reference(x => x.CarModeli.Marka).Load();
      _dbContext.Entry(araba).Reference(y => y.Yakit).Load();
      _dbContext.Entry(araba).Reference(x => x.Vites).Load();
      
      return araba.ToArabaDto();
    }

    public bool UpdateAraba(int id, ArabaCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAraba(int id)
    {
        throw new NotImplementedException();
    }
}