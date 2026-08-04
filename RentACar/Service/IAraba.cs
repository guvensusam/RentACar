using RentACar.DTOs;

namespace RentACar.Service;

public interface IAraba
{
    IEnumerable<ArabaResponseDto>  GetAllAraba(ArabaFilterDto filter);
    ArabaResponseDto GetArabaById(int arabaId);
    
    ArabaResponseDto CreateAraba(ArabaCreateDto dto);

    bool UpdateAraba(int id, ArabaCreateDto dto);
    
    bool DeleteAraba(int id);
    
}