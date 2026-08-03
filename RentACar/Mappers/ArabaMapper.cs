using System.Runtime.Intrinsics.Arm;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class ArabaMapper
{
    public static ArabaResponseDto ToArabaDto(this Araba araba)
    {
        return new ArabaResponseDto()
        {
            ArabaId = araba.Id,
            ArabaAdi = araba.ArabaAdi,
            ArabaFiyat =  araba.ArabaFiyat,
            ArabaYasi =  araba.ArabaYasi,
            ToplamKm =  araba.ToplamKm,
            
            MarkaId = araba.CarModeli.MarkaId,
            MarkaAdi = araba.CarModeli.Marka.MarkaAdi,
            
            ModelAdi =  araba.CarModeli.ModelAdi,
            ModelId = araba.CarModeli.Id,
            
            VitesId = araba.VitesID,
            VitesTuru = araba.Vites.VitesTuru,
            
            YakitId =  araba.YakitID,
            YakitAdi = araba.Yakit.YakitAdi,
        };
    }
}