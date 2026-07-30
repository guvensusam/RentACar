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
 public ActionResult<IEnumerable<MarkaResponseDto>> GetAll()
 {
    return Ok(_service.GetAllVites());
 }

 [HttpPost]
 public ActionResult<VitesResponseDto> Create(VitesCreateDto dto)
 {
   return Ok(_service.Create(dto));
   
 }
 
 
}