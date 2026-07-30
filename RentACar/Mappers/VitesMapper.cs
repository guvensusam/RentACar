using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class VitesMapper
{
    public static VitesResponseDto ToVitesDto( this Vites vites )
    {
        return new VitesResponseDto()
        {
            VitesId = vites.VitesId,
            VitesTuru = vites.VitesTuru,
        };
    }
}