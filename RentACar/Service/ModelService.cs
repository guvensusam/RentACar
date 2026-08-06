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
    
    public async Task<IEnumerable<CarModelResponseDto>> GetAllModel()
    {
       return await _context.CarModelleri
           .Include(x=>x.Marka)
           .Select(x=>x.ToModelDto())
           .ToListAsync();
    }

    public async Task<CarModelResponseDto> GetByIdModel(int modelId)
    {
      var  model = await _context.CarModelleri
          .Include(x=>x.Marka)
          .FirstOrDefaultAsync(model => model.Id == modelId);
          
      if (model == null)
      {
          return null;
      }
      return model.ToModelDto();
    }

    public async Task<CarModelResponseDto> CreateModel(CarModelCreateDto modelCreateDto)
    {
        var create = new CarModeli()
        {
            MarkaId = modelCreateDto.MarkaId,
            ModelAdi = modelCreateDto.ModelAdi,
        };
        _context.CarModelleri.Add(create);
       await  _context.SaveChangesAsync();
        return create.ToModelDto();
    }

    public async Task<bool> UpdateModel(int modelId, CarModelCreateDto modelCreateDto)
    {
      var  model =await  _context.CarModelleri.FindAsync(modelId);
      if (model == null)
      {
          return false;
      }
      model.ModelAdi = modelCreateDto.ModelAdi;
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteModel(int modelId)
    {
        var model =await _context.CarModelleri.FindAsync(modelId);
        if (model == null)
        {
            return false;
            
        }
        _context.CarModelleri.Remove(model);
       await  _context.SaveChangesAsync();
        return true;
    }
}