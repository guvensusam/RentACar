using RentACar.Model;

namespace RentACar.DTOs;

public class RentalResponseDto
{
    public int RentalId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal DailyPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public RentalStatus Status { get; set; }=RentalStatus.Acik;
    public DateTime CreateAt { get; set; }=DateTime.Now;

    public int ArabaId { get; set; }
    public int UserId { get; set; }

}
