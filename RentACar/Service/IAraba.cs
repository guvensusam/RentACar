using RentACar.DTOs;

namespace RentACar.Service;

public interface IAraba
{
    Task<PagedResponse<ArabaResponseDto>>  GetAllAraba(ArabaFilterDto filter, int page = 1, int pageSize = 10);
    Task<ArabaResponseDto> GetArabaById(int arabaId);
    
    Task<ArabaResponseDto> CreateAraba(ArabaCreateDto dto);

    Task<bool> UpdateAraba(int id, ArabaCreateDto dto);
    
    Task<bool> DeleteAraba(int id);
    
}