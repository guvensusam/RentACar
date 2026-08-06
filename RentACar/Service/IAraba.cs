using RentACar.DTOs;

namespace RentACar.Service;

public interface IAraba
{
    Task<IEnumerable<ArabaResponseDto>>  GetAllAraba(ArabaFilterDto filter);
    Task<ArabaResponseDto> GetArabaById(int arabaId);
    
    Task<ArabaResponseDto> CreateAraba(ArabaCreateDto dto);

    Task<bool> UpdateAraba(int id, ArabaCreateDto dto);
    
    Task<bool> DeleteAraba(int id);
    
}