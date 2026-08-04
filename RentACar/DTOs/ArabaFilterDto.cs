namespace RentACar.DTOs;

public class ArabaFilterDto
{
    public int? ModelId { get; set; }
    public int? MarkaId { get; set; }
    public int? VitesId { get; set; }
    public int? YakitId { get; set; }
    
    public int? MinYil { get; set; }
    public int? MaxYil { get; set; }
    
    public float? MaxKm { get; set; }
    public float? MinKm { get; set; }
    
    public decimal? MinFiyat { get; set; }
    public decimal? MaxFiyat { get; set; }
}


