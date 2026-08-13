using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class RentalService : IRental
{
    
    private readonly RentACarDbContext _context;
    
    public RentalService(RentACarDbContext context)
    {
        _context = context;
    }
    
    public async Task<RentalResponseDto> CreateRental(RentalCreateDto rentalCreateDto,int userId)
    {
        var araba= await _context.Arabalarr.
            FirstOrDefaultAsync(x => x.Id ==rentalCreateDto.ArabaId);
        if (araba == null)
        {
            throw new Exception("Araba mevcut degil");
        }

        if (rentalCreateDto.StartDate >= rentalCreateDto.EndDate || rentalCreateDto.StartDate < DateTime.Now)
        {
            throw new Exception("Gecerli Tarih Girin ");
        }
        var cakisanRentalVarMi = await _context.Rentals.AnyAsync(x =>
            x.ArabaId == rentalCreateDto.ArabaId &&
            x.Status == RentalStatus.Acik &&
            x.StartDate < rentalCreateDto.EndDate &&
            x.EndDate > rentalCreateDto.StartDate);

        if (cakisanRentalVarMi)
        {
            throw new Exception("Araç bu tarihlerde müsait değil");
        }

        var gunSayisi = (rentalCreateDto.EndDate - rentalCreateDto.StartDate).Days;
        var totalPrice = araba.ArabaFiyat * gunSayisi;

        var rental = new Rental()
        {
            ArabaId = rentalCreateDto.ArabaId,
            UserId = userId,
            StartDate = rentalCreateDto.StartDate,
            EndDate = rentalCreateDto.EndDate,
            TotalPrice = totalPrice,
            DailyPrice = araba.ArabaFiyat,

        };
        
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        return rental.ToRentalDto();
    }
}