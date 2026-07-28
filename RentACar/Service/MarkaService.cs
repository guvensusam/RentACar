using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class MarkaService : IMarka
{
    private readonly RentACarDbContext _context;
    
    public MarkaService(RentACarDbContext context)
    {
        _context = context;
    }
    
    
    
    public IEnumerable<MarkaResponseDto> GetAllMarka()
    {
        return _context.Markalar
            .Select(x=>x.ToDto())
            .ToList();
    }
   
    public MarkaResponseDto GetByIdMarka(int markaId)
    {
        var marka = _context.Markalar.FirstOrDefault(x=>x.MarkaId == markaId);
        if (marka == null)
        {
            return null;
        }
        return marka.ToDto();
    }

    public Marka CreateMarka(MarkaCreateDto marka)
    {
        var markaekle = new Marka()
        {
            MarkaAdi = marka.MarkaAdi,
        };
        
        _context.Markalar.Add(markaekle);
        _context.SaveChanges();
        return markaekle;
    }
     
    public bool UpdateMarka(int MarkaId, MarkaResponseDto markaResponse)
    {
        var Marka= _context.Markalar.Find(MarkaId);
        if (Marka == null)
        {
            return false;
        }
        Marka.MarkaAdi = markaResponse.MarkaAdi;
        _context.Markalar.Update(Marka);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteMarka(int Id)
    {
        var marka = _context.Markalar.FirstOrDefault(x=>x.MarkaId == Id);
        if (marka == null)
        {
            return false;
        }
       _context.Markalar.Remove(marka);
       _context.SaveChanges();
        return true;
    }
    
}