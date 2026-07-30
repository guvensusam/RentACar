using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.DTOs;
using RentACar.Mappers;
using RentACar.Model;

namespace RentACar.Service;

public class ModelService : IModel
{
    
    private readonly RentACarDbContext _context;
    public ModelService(RentACarDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<CarModelResponseDto> GetAllModel()
    {
       return _context.CarModelleri
           .Include(x=>x.Marka)
           .Select(x=>x.ToModelDto())
           .ToList();
    }

    public CarModelResponseDto GetByIdModel(int modelId)
    {
      var  model = _context.CarModelleri
          .Include(x=>x.Marka)
          .FirstOrDefault(model => model.Id == modelId);
          
      if (model == null)
      {
          return null;
      }
      return model.ToModelDto();
    }

    public CarModelResponseDto CreateModel(CarModelCreateDto modelCreateDto)
    {
        var create = new CarModeli()
        {
            MarkaId = modelCreateDto.MarkaId,
            ModelAdi = modelCreateDto.ModelAdi,
        };
        _context.CarModelleri.Add(create);
        _context.SaveChanges();
        return create.ToModelDto();
    }

    public bool UpdateModel(int modelId, CarModelCreateDto modelCreateDto)
    {
      var  model = _context.CarModelleri.Find(modelId);
      if (model == null)
      {
          return false;
      }
      model.ModelAdi = modelCreateDto.ModelAdi;
      _context.SaveChanges();
      return true;
    }

    public bool DeleteModel(int modelId)
    {
        var model =_context.CarModelleri.Find(modelId);
        if (model == null)
        {
            return false;
            
        }
        _context.CarModelleri.Remove(model);
        _context.SaveChanges();
        return true;
    }
}