using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class YakitService : IYakit
{
    private readonly RentACarDbContext _context;
    
    public YakitService(RentACarDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<YakitResponseDto>> GetAllYakitAsync()
    {
        return await _context.Yakitlar
            .Select(x => x.ToYakitDto())
            .ToListAsync();
    }

    public async Task<YakitResponseDto> CreateAsync(YakitCreateDto dto)
    {
        var create = new Yakit()
        {
            YakitAdi = dto.YakitAdi,
        };
        _context.Yakitlar.Add(create);
        await _context.SaveChangesAsync();
        return create.ToYakitDto();
    }
}