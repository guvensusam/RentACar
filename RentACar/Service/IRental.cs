using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;

namespace RentACar.Service;

public interface IRental
{
     Task<RentalResponseDto> CreateRental (RentalCreateDto rentalCreateDto, int userId);

     Task<IEnumerable<RentalResponseDto>> GetMyRentals(int userId);

     Task<RentalResponseDto> GetRentalById(int id, int userId);

     Task<RentalResponseDto> CancelRental(int id, int userId);

     Task<IEnumerable<RentalResponseDto>> GetAllRentals();
}