using RentACar.DTOs;

namespace RentACar.Service;

public interface IVites
{
    Task<IEnumerable<VitesResponseDto>> GetAllVitesAsync();
    
    Task<VitesResponseDto> CreateAsync(VitesCreateDto dto);
}