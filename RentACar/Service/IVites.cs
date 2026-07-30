using RentACar.DTOs;

namespace RentACar.Service;

public interface IVites
{
    IEnumerable<VitesResponseDto> GetAllVites();
    
    VitesResponseDto Create(VitesCreateDto dto);
}