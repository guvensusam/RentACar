using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class YakitMapper
{
    public  static YakitResponseDto ToYakitDto(this  Yakit yakit)
    {
        return new YakitResponseDto()
        {
            YakitAdi = yakit.YakitAdi,
            YakitId = yakit.YakitId,
        };
    }
}