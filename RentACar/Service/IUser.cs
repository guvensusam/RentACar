using RentACar.DTOs;

namespace RentACar.Service;

public interface IUser
{
   public  Task<UserResponseDto> RegisterAsync(UserRegisterDto user);
   public  Task<LoginResponseDto?> LoginAsync(UserLoginDto user);

}