using System.ComponentModel.DataAnnotations;

namespace RentACar.Model;

public class CarModeli
{
    
    public int Id { get; set; }
    public string ModelAdi { get; set; } = string.Empty;
    
  
   public Marka Marka { get; set; }
   public int MarkaId { get; set; }
   
   
   public List<Araba> Arabalar { get; set; }
    
}


