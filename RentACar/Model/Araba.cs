using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Model;

public class Araba
{
    public int Id { get; set; }
    public int ArabaYasi { get; set; } 
    public string ArabaAdi { get; set; } =  string.Empty;
    public double ArabaFiyat { get; set; }
    public float ToplamKm { get; set; }
    
    public Marka Marka { get; set; }
      
    
    public Vites Vites { get; set; }
    [ForeignKey(nameof(Vites))]
    public int? VitesID { get; set; }
    
    public Yakit Yakit { get; set; }
    [ForeignKey(nameof(Yakit))]
    public int? YakitID { get; set; }
    
}