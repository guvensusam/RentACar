using RentACar.DTOs;

namespace RentACar.Service;

public interface IModel
{
    Task<IEnumerable<CarModelResponseDto>> GetAllModel();
    
    Task<CarModelResponseDto> GetByIdModel(int modelId);
    
    Task<CarModelResponseDto> CreateModel(CarModelCreateDto modelCreateDto);
    
    public Task<bool>  UpdateModel(int modelId,CarModelCreateDto modelCreateDto);
    
    Task<bool> DeleteModel(int modelId);
}