using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;
using RentACar.Service;

namespace RentACar.Controller;

[ApiController]
[Route("api/[controller]")]
public class MarkaController : ControllerBase
{
    private readonly IMarka _service;
    public MarkaController(IMarka service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarkaResponseDto>>> Get()
    {
        return Ok(await _service.GetAllMarka());
    }

    
    [HttpGet("{MarkaId}")]
    public async Task<ActionResult<MarkaResponseDto>> Get(int MarkaId)
    {
        var marka = await _service.GetByIdMarka(MarkaId);
        if (marka == null)
        {
            return NotFound();
        }
       return Ok(marka);
    }
    
    [HttpPost]
    public async Task<ActionResult<MarkaResponseDto>> Create(MarkaCreateDto markaResponse)
    {
        var markaekle = await _service.CreateMarka(markaResponse);
        return Ok(markaekle);
    }

    [HttpPut("{MarkaId}")]
    public async Task<ActionResult<MarkaResponseDto>> Update(int MarkaId, MarkaCreateDto markaResponse)
    { 
        var updated = await _service.UpdateMarka(MarkaId, markaResponse); 
        return Ok(updated);
    }
    
    [HttpDelete("{MarkaId}")]
    public async Task< ActionResult<MarkaResponseDto> > Delete(int MarkaId){
        var deleted =await  _service.DeleteMarka(MarkaId);
        return Ok(deleted);}
}