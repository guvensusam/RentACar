using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    
    
    public async Task<IEnumerable<MarkaResponseDto>> GetAllMarka()
    {
        return await _context.Markalar
            .Select(x=>x.ToDto())
            .ToListAsync();
    }
   
    public async Task<MarkaResponseDto> GetByIdMarka(int markaId)
    {
        var marka = await _context.Markalar.FirstOrDefaultAsync(x=>x.MarkaId == markaId);
        if (marka == null)
        {
            return null;
        }
        return marka.ToDto();
    }

    public async Task<MarkaResponseDto> CreateMarka(MarkaCreateDto marka)
    {
        var markaekle = new Marka()
        {
            MarkaAdi = marka.MarkaAdi,
        };
        
        _context.Markalar.Add(markaekle);
        await _context.SaveChangesAsync();
        return markaekle.ToDto();
    }
     
    public async Task<bool> UpdateMarka(int MarkaId, MarkaCreateDto markaResponse)
    {
        var marka= await _context.Markalar.FindAsync(MarkaId);
        if (marka == null)
        {
            return false;
        }
        marka.MarkaAdi = markaResponse.MarkaAdi;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMarka(int Id)
    {
        var marka = await _context.Markalar.FirstOrDefaultAsync(x=>x.MarkaId == Id);
        if (marka == null)
        {
            return false;
        }
       _context.Markalar.Remove(marka);
       await _context.SaveChangesAsync();
        return true;
    }
    
}