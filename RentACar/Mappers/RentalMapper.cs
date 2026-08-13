using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class RentalMapper
{
    public static RentalResponseDto ToRentalDto (this Rental rental)
    {
        return new RentalResponseDto()
        {
            ArabaId = rental.ArabaId,
            UserId = rental.UserId,
            RentalId = rental.RentalId,
            
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            
            DailyPrice = rental.DailyPrice,
            TotalPrice = rental.TotalPrice,
            Status = rental.Status,
            CreateAt = rental.CreateAt,
        };
    }
}