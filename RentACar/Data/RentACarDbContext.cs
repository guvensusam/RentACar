using Microsoft.EntityFrameworkCore;
using RentACar.Model;

namespace RentACar.Data;


public class RentACarDbContext : DbContext
{
    public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options)
    {
        
    }
    
    
    public DbSet<Araba> Arabalarr { get; set; }
    public DbSet<CarModeli> CarModelleri{ get; set; }
    public DbSet<Marka> Markalar { get; set; }
    public DbSet<Vites> Vitesler { get; set; }
    public DbSet<Yakit> Yakitlar { get; set; }
    
    public DbSet<User>  Users { get; set; }
    
    public DbSet<Rental> Rentals { get; set; }
    
}