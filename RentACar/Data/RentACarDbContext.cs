using Microsoft.EntityFrameworkCore;
using RentACar.Model;

namespace RentACar.Data;


public class RentACarDbContext : DbContext
{
    public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options)
    {
        
    }
    
    
    public DbSet<Araba> Arabalar { get; set; }
    public DbSet<CarModeli> CarModeli { get; set; }
    public DbSet<Marka> Marka { get; set; }
    public DbSet<Vites> Vites { get; set; }
    public DbSet<Yakit> Yakit { get; set; }
    
    
}