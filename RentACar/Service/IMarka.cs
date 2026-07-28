using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Service;

public interface IMarka
{
    IEnumerable<MarkaDto> GetAllMarka();
    
    MarkaDto GetByIdMarka(int markaId);
    
    Marka CreateMarka(MarkaDto marka);
    
    public bool  UpdateMarka(int MarkaId,MarkaDto marka);
    
    bool DeleteMarka(int markaId);
    
    
}