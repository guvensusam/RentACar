using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;

namespace RentACar.Service;

public interface IRental
{
     Task<RentalResponseDto> CreateRental (RentalCreateDto rentalCreateDto, int userId);

     Task<PagedResponse<RentalResponseDto>> GetMyRentals(int userId, int page = 1, int pageSize = 10);

     Task<RentalResponseDto> GetRentalById(int id, int userId);

     Task<RentalResponseDto> CancelRental(int id, int userId);

     Task<PagedResponse<RentalResponseDto>> GetAllRentals(int page = 1, int pageSize = 10);
}
