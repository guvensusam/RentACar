namespace RentACar.Model;

public class Marka
{
    public int MarkaId { get; set; }
    public string MarkaAdi { get; set; }= string.Empty;
    
    
    
    
    
    
    public List<Araba> Arabalar { get; set; }
    
    public List<CarModeli> CarModelleri { get; set; }
}