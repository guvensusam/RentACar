using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class MarkaMapper
{
    public static MarkaDto ToDto(this Marka marka)
    {
        return new MarkaDto()
        {
            MarkaAdi = marka.MarkaAdi,
        };
    }
}