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
    public ActionResult<IEnumerable<MarkaResponseDto>> Get()
    {
        return Ok(_service.GetAllMarka());
    }

    
    [HttpGet("{MarkaId}")]
    public ActionResult<MarkaResponseDto> Get(int MarkaId)
    {
        var marka = _service.GetByIdMarka(MarkaId);
        if (marka == null)
        {
            return NotFound();
        }
       return Ok(marka);
    }
        
        
        
        
    [HttpPost]
    public ActionResult<MarkaResponseDto> Create(MarkaCreateDto markaResponse)
    {
        var markaekle = _service.CreateMarka(markaResponse);
        return Ok(markaekle);
    }

    [HttpPut("{MarkaId}")]
    public ActionResult<MarkaResponseDto> Update(int MarkaId, MarkaResponseDto markaResponse)
    {
        var updated = _service.UpdateMarka(MarkaId, markaResponse);
        return Ok(updated);
    }
    
    [HttpDelete("{MarkaId}")]
    public ActionResult<MarkaResponseDto> Delete(int MarkaId){
        var deleted = _service.DeleteMarka(MarkaId);
        return Ok(deleted);}
}