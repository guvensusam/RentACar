using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUser _user;
    
    public  AuthController(IUser user)
    {
      _user = user;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserRegisterDto userRegisterDto)
    {
        var sonuc = await _user.RegisterAsync(userRegisterDto);
        return Ok(sonuc);              
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserLoginDto>> Login(UserLoginDto userLoginDto)
    {
        var sonuc = await _user.LoginAsync(userLoginDto);
        if (sonuc == null)
        {
            return Unauthorized("Email veya şifre hatalı.");
        }
        return Ok(sonuc);
    }
}