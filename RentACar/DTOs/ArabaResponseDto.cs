namespace RentACar.DTOs;

public class ArabaResponseDto
{
    public int ArabaId { get; set; }
    public int ArabaYasi { get; set; } 
    public string ArabaAdi { get; set; } =  string.Empty;
    public double ArabaFiyat { get; set; }
    public float ToplamKm { get; set; }
    
    public int MarkaId { get; set; }
    public string MarkaAdi { get; set; }
    
    public int ModelId { get; set; }
    public string ModelAdi { get; set; }
    
    public int VitesId { get; set; }
    public string VitesTuru { get; set; }
    
    public int YakitId { get; set; }
    public string YakitAdi { get; set; }
}