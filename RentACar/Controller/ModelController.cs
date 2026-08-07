using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;

using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private readonly IModel _service;
    
    public ModelController(IModel service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarModelResponseDto>>> GetAll()
    {
        return Ok(await _service.GetAllModel());
    }

    [HttpGet("{modelId:int}")]
    public async Task<ActionResult<CarModelResponseDto>> Get(int modelId)
    {
       var model = await _service.GetByIdModel(modelId);
       if (model == null)
       {
           return NotFound();
           
       }
       return Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult<CarModelResponseDto>> Create(CarModelCreateDto model)
    {
        var create = await _service.CreateModel(model);
        return Ok(create);
    }

    [HttpPut("{modelId:int}")]
    public async Task<ActionResult<CarModelResponseDto>> Update(int modelId,CarModelCreateDto model)

    {
        var uptade = await _service.UpdateModel(modelId, model);
        return Ok(uptade);

    }

    [HttpDelete("{modelId:int}")]
    public async Task<ActionResult<CarModelResponseDto>> Delete(int modelId)
    {
        var delete = await _service.DeleteModel(modelId);
        return Ok(delete);
    }

}