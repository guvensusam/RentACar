using System.ComponentModel.DataAnnotations;

namespace RentACar.DTOs;

public class ArabaCreateDto
{
    public int ArabaYasi { get; set; } 
    [Required]
    public string ArabaAdi { get; set; } =  string.Empty;
    public decimal ArabaFiyat { get; set; }
    public float ToplamKm { get; set; }
    
    public int ModelId { get; set; }
    public int VitesId { get; set; }
    public int YakitId { get; set; }
}