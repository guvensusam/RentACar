using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Mappers;

public static class RentalMapper
{
    public static RentalResponseDto ToRentalDto (this Rental rental)
    {
        var gosterilecekStatus = rental.Status;

        if (gosterilecekStatus == RentalStatus.Acik && rental.EndDate < DateTime.Now)
        {
            gosterilecekStatus = RentalStatus.Tamamlandi;
        }

        return new RentalResponseDto()
        {
            RentalId = rental.RentalId,

            StartDate = rental.StartDate,
            EndDate = rental.EndDate,

            DailyPrice = rental.DailyPrice,
            TotalPrice = rental.TotalPrice,

            Status = gosterilecekStatus,
            CreateAt = rental.CreateAt,

            ArabaId = rental.ArabaId,
            UserId = rental.UserId,
        };
    }
}
