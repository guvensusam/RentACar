using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class VitesService : IVites
{
    private readonly RentACarDbContext _context;
    
    public VitesService(RentACarDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<VitesResponseDto>> GetAllVitesAsync()
    {
        return await _context.Vitesler
            .Select(x => x.ToVitesDto())
            .ToListAsync();
    }

    public async Task<VitesResponseDto> CreateAsync(VitesCreateDto dto)
    {
        var  create = new Vites()
        {
            VitesTuru = dto.VitesTuru,
        };
         _context.Vitesler.Add(create);
        await _context.SaveChangesAsync();
        return  create.ToVitesDto();
    }
}