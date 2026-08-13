using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Model;

public class Rental
{
   public int RentalId { get; set; }
   
   public DateTime StartDate { get; set; }
   public DateTime EndDate { get; set; }
   
   public decimal DailyPrice { get; set; }
   public decimal TotalPrice { get; set; }
   
   public RentalStatus Status { get; set; }=RentalStatus.Acik;
   
   
   public DateTime CreateAt { get; set; }=DateTime.Now;
  
   
   public User User { get; set; }
   [ForeignKey(nameof(User))] 
   public int UserId { get; set; }
    
   
   public Araba Araba { get; set; }
   [ForeignKey(nameof(Araba))]
   public int ArabaId { get; set; }
      
    
    
    
}