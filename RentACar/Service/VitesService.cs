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
    
    
    public IEnumerable<VitesResponseDto> GetAllVites()
    {
        return _context.Vitesler
            .Select(x => x.ToVitesDto())
            .ToList();
    }

    public VitesResponseDto Create(VitesCreateDto dto)
    {
        var create = new Vites()
        {
            VitesTuru = dto.VitesTuru,
        };
        _context.Vitesler.Add(create);
        _context.SaveChanges();
        return create.ToVitesDto();
    }
}