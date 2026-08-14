using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Service;
using System.Security.Claims;

namespace RentACar.Controller;

[ApiController]
[Route("api/[controller]")]
public class RentalController : ControllerBase
{
    private readonly IRental _services;

    public RentalController(IRental services)
    {
        _services = services;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RentalResponseDto>> CreateRental(RentalCreateDto rentalCreateDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var sonuc = await _services.CreateRental(rentalCreateDto, userId);
        return Ok(sonuc);
    }

    [HttpGet("my-rentals")]
    [Authorize]
    public async Task<ActionResult<PagedResponse<RentalResponseDto>>> GetMyRentals(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var sonuc = await _services.GetMyRentals(userId, page, pageSize);
        return Ok(sonuc);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<RentalResponseDto>> GetRentalById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var sonuc = await _services.GetRentalById(id, userId);
        return Ok(sonuc);
    }

    [HttpPut("{id}/cancel")]
    [Authorize]
    public async Task<ActionResult<RentalResponseDto>> CancelRental(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var sonuc = await _services.CancelRental(id, userId);
        return Ok(sonuc);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PagedResponse<RentalResponseDto>>> GetAllRentals(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var sonuc = await _services.GetAllRentals(page, pageSize);
        return Ok(sonuc);
    }
}
