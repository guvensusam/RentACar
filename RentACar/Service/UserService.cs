using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Service;

public class UserService : IUser
{
    private readonly RentACarDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public UserService(RentACarDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }


    public async Task<UserResponseDto> RegisterAsync(UserRegisterDto dto)
    {

        var yeniUser = new User()
        {
            AdSoyad = dto.AdSoyad,
            Email = dto.Email,
            Role = "Musteri"
        };

        var hasher = new PasswordHasher<User>();
        yeniUser.PasswordHash = hasher.HashPassword(yeniUser, dto.Password);

        _dbContext.Users.Add(yeniUser);
        await _dbContext.SaveChangesAsync();
        return new UserResponseDto()
        {
            UserId = yeniUser.UserId,
            Email = yeniUser.Email,
            AdSoyad = yeniUser.AdSoyad,
            Role = yeniUser.Role

        };
    }

    public async Task<LoginResponseDto?> LoginAsync(UserLoginDto dto)
    {
       
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
        {
            return null; 
        }

        
        var hasher = new PasswordHasher<User>();
        var sonuc = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (sonuc == PasswordVerificationResult.Failed)
        {
            return null;
        }

        
        var token = TokenUret(user);

        return new LoginResponseDto
        {
            Token = token,
            Email = user.Email,
            Role = user.Role
        };
    }

    private string TokenUret(User user)
    {
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        
        var secretKey = _configuration["Jwt:SecretKey"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}