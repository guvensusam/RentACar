
using RentACar.DTOs;


namespace RentACar.Service;

public interface IMarka
{
    Task<IEnumerable<MarkaResponseDto>> GetAllMarka();
    
    Task<MarkaResponseDto> GetByIdMarka(int markaId);
    
    Task<MarkaResponseDto> CreateMarka(MarkaCreateDto markaResponse);
    
    public Task<bool>  UpdateMarka(int markaId,MarkaCreateDto markaResponse);
    
    Task<bool> DeleteMarka(int markaId);
    
    
}