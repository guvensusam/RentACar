using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private readonly ModelService _service;
    
    public ModelController(ModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CarModelResponseDto>> GetAll()
    {
        return Ok(_service.GetAllModel());
    }

    [HttpGet("{modelId:int}")]
    public ActionResult<CarModelResponseDto> Get(int modelId)
    {
       var model = _service.GetByIdModel(modelId);
       if (model == null)
       {
           return NotFound();
           
       }
       return Ok(model);
    }

    [HttpPost]
    public ActionResult<CarModelResponseDto> Create(CarModelCreateDto model)
    {
        var create = _service.CreateModel(model);
        return Ok(create);
    }

    [HttpPut("{modelId:int}")]
    public ActionResult<CarModelResponseDto> Update(int modelId,CarModelCreateDto model)

    {
        var uptade = _service.UpdateModel(modelId, model);
        return Ok(uptade);

    }

    [HttpDelete("{modelId:int}")]
    public ActionResult<CarModelResponseDto> Delete(int modelId)
    {
        var delete = _service.DeleteModel(modelId);
        return Ok(delete);
    }

}