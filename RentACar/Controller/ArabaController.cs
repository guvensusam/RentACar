using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class ArabaController : ControllerBase
{
    private readonly IAraba _services;
    
    public ArabaController(IAraba services)
    {
       _services = services;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ArabaResponseDto>> GetAll([FromQuery] ArabaFilterDto filter)
    {
        return Ok(_services.GetAllAraba(filter));
    }

    [HttpPost]

    [HttpPost]
    public ActionResult<ArabaResponseDto> CreateAraba(ArabaCreateDto arabaCreateDto)
    {
        var sonuc = _services.CreateAraba(arabaCreateDto);
        if (sonuc == null)
            return BadRequest("Geçersiz CarModeliId, VitesId ya da YakitId.");

        return Ok(sonuc);
    }

    [HttpGet("{id}")]
    public ActionResult<ArabaResponseDto> GetArabaById(int id)
    {
        return Ok(_services.GetArabaById(id));
    }

    [HttpPut("{id}")]
    public bool UpdateAraba(int id, ArabaCreateDto arabaCreateDto)
    {
        return _services.UpdateAraba(id, arabaCreateDto);
    }
    
    [HttpDelete("{id}")]
    public bool DeleteAraba(int id)
    {
        return _services.DeleteAraba(id);
    }
    
}