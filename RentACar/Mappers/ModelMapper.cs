using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class ModelMapper
{
    public static CarModelResponseDto ToModelDto(this CarModeli carModel)
    {
        return new CarModelResponseDto()
        {
            Id = carModel.Id,
            ModelAdi = carModel.ModelAdi,
            MarkarId = carModel.MarkaId,
        };
    }
        
}