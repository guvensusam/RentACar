using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class MarkaMapper
{
    public static MarkaResponseDto ToDto(this Marka marka)
    {
        return new MarkaResponseDto()
        {
            MarkaAdi = marka.MarkaAdi,
            MarkaId =  marka.MarkaId,
            
        };
    }
}