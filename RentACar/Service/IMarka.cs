using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Service;

public interface IMarka
{
    Task<IEnumerable<MarkaResponseDto>> GetAllMarka();
    
    Task<MarkaResponseDto> GetByIdMarka(int markaId);
    
    Task<MarkaResponseDto> CreateMarka(MarkaCreateDto markaResponse);
    
    public Task<bool>  UpdateMarka(int MarkaId,MarkaCreateDto markaResponse);
    
    Task<bool> DeleteMarka(int markaId);
    
    
}