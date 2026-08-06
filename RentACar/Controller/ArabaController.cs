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
    public async Task<ActionResult<IEnumerable<ArabaResponseDto>>> GetAll([FromQuery] ArabaFilterDto filter)
    {
        return  Ok(await _services.GetAllAraba(filter));
    }
    
  
     
    [HttpPost]
    public async Task<ActionResult<ArabaResponseDto>> CreateAraba(ArabaCreateDto arabaCreateDto)
    {
        var sonuc = await _services.CreateAraba(arabaCreateDto);
        if (sonuc == null)
            return BadRequest("Geçersiz CarModeliId, VitesId ya da YakitId.");

        return Ok(sonuc);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArabaResponseDto>> GetArabaById(int id)
    {
        return Ok(await _services.GetArabaById(id));
    }

    [HttpPut("{id}")]
    public async Task<bool> UpdateAraba(int id, ArabaCreateDto arabaCreateDto)
    {
        return await  _services.UpdateAraba(id, arabaCreateDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<bool> DeleteAraba(int id)
    {
        return await _services.DeleteAraba(id);
    }
    
}