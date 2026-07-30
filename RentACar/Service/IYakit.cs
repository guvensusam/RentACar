using RentACar.DTOs;

namespace RentACar.Service;

public interface IYakit
{
    IEnumerable<YakitResponseDto> GetAllYakit();
    
    YakitResponseDto Create(YakitCreateDto dto);
}