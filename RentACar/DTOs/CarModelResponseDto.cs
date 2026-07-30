namespace RentACar.DTOs;

public class CarModelResponseDto
{
 
    public int Id { get; set; }
    public string ModelAdi { get; set; } = string.Empty;
    public int MarkaId { get; set; }
    public  string MarkaAdi { get; set; } = string.Empty;
}