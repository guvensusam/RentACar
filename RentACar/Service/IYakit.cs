using RentACar.DTOs;

namespace RentACar.Service;

public interface IYakit
{
    Task<IEnumerable<YakitResponseDto>> GetAllYakitAsync();
    
    Task<YakitResponseDto> CreateAsync(YakitCreateDto dto);
}