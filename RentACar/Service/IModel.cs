using RentACar.DTOs;

namespace RentACar.Service;

public interface IModel
{
    IEnumerable<CarModelResponseDto> GetAllModel();
    
    CarModelResponseDto GetByIdModel(int modelId);
    
    CarModelResponseDto CreateModel(CarModelCreateDto modelCreateDto);
    
    public bool  UpdateModel(int modelId,CarModelCreateDto modelCreateDto);
    
    bool DeleteModel(int modelId);
}