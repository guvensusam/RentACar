using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IUser _user;
    
    public  RegisterController(IUser user)
    {
      _user = user;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserRegisterDto userRegisterDto)
    {
        var sonuc = await _user.RegisterAsync(userRegisterDto);
        return Ok(sonuc);
    }
    
}