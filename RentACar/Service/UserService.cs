using Microsoft.AspNetCore.Identity;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Model;

namespace RentACar.Service;

public class UserService : IUser
{
    private readonly RentACarDbContext _dbContext;
    
    public UserService(RentACarDbContext dbContext)
    {
        _dbContext = dbContext;
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
}