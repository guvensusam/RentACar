using RentACar.DTOs;

namespace RentACar.Service;

public interface IUser
{
    Task<UserResponseDto> RegisterAsync(UserRegisterDto user);


}