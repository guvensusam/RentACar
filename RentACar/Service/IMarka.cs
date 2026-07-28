using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Service;

public interface IMarka
{
    IEnumerable<MarkaResponseDto> GetAllMarka();
    
    MarkaResponseDto GetByIdMarka(int markaId);
    
    Marka CreateMarka(MarkaCreateDto markaResponse);
    
    public bool  UpdateMarka(int MarkaId,MarkaResponseDto markaResponse);
    
    bool DeleteMarka(int markaId);
    
    
}