using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Service;

namespace RentACar.Controller;
[ApiController]
[Route("api/[controller]")]
public class VitesController : ControllerBase

{
 private readonly IVites _service;
 
 public  VitesController(IVites service)
    {
        _service = service;
    }
 
 [HttpGet]
 public async Task<ActionResult<IEnumerable<VitesResponseDto>>> GetAll()
 {
    return Ok(await _service.GetAllVitesAsync());
 }

 [HttpPost]
 [Authorize(Roles = "Admin")]
 public async Task <ActionResult<VitesResponseDto>> Create(VitesCreateDto dto)
 {
   return Ok(await _service.CreateAsync(dto));
   
 }
 
 
}