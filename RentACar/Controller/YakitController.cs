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
    public async Task<ActionResult<IEnumerable<YakitResponseDto>>> GettAll()
    {
        return Ok(await _service.GetAllYakitAsync());
    }

    [HttpPost]
    public async Task<ActionResult<YakitResponseDto>> Create(YakitCreateDto yakitCreate)
    {
        return Ok(await _service.CreateAsync(yakitCreate));
    }
    
}