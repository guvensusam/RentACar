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

    public IEnumerable<YakitResponseDto> GetAllYakit()
    {
        return _context.Yakitlar
            .Select(x => x.ToYakitDto())
            .ToList();
    }

    public YakitResponseDto Create(YakitCreateDto dto)
    {
        var create = new Yakit()
        {
            YakitAdi = dto.YakitAdi,
        };
        _context.Yakitlar.Add(create);
        _context.SaveChanges();
        return create.ToYakitDto();
    }
}