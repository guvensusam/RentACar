using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Model;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class YakitController : ControllerBase
{
    private readonly IYakit _service;

    public YakitController(IYakit service)
    {
        _service = service;
    }



    [HttpGet]
    public ActionResult<IEnumerable<YakitResponseDto>> GettAll()
    {
        return Ok(_service.GetAllYakit());
    }

    [HttpPost]
    public ActionResult<YakitResponseDto> Create(YakitCreateDto yakitCreate)
    {
        return Ok(_service.Create(yakitCreate));
        
    }
    
}