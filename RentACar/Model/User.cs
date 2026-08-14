namespace RentACar.Model;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }=string.Empty;  
    public string PasswordHash { get; set; }=string.Empty;
    public string AdSoyad { get; set; }=string.Empty;
    public string Role { get; set; } = "Musteri";
    public ICollection<Rental> Rentals { get; set; }
}