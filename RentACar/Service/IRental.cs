using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;

namespace RentACar.Service;

public interface IRental
{
     Task<RentalResponseDto> CreateRental (RentalCreateDto rentalCreateDto, int userId); 
}