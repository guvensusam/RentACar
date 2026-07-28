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
    public ActionResult<IEnumerable<Marka>> Get()
    {
        return Ok(_service.GetAllMarka());
    }
        
        
        
        
        
        
        
    [HttpPost]
    public ActionResult<MarkaDto> Create(MarkaDto marka)
    {
        var markaekle = _service.CreateMarka(marka);
        return Ok(markaekle);
    }
}