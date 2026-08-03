namespace RentACar.DTOs;

public class ArabaCreateDto
{
    public int ArabaYasi { get; set; } 
    public string ArabaAdi { get; set; } =  string.Empty;
    public double ArabaFiyat { get; set; }
    public float ToplamKm { get; set; }
    
    public int ModelId { get; set; }
    public int VitesId { get; set; }
    public int YakitId { get; set; }
}