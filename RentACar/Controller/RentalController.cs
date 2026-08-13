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

        try
        {
            var sonuc = await _services.CreateRental(rentalCreateDto, userId);
            return Ok(sonuc);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}