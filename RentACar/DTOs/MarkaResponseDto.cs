using System.ComponentModel.DataAnnotations.Schema;
using RentACar.Model;

namespace RentACar.DTOs;

public class MarkaResponseDto
{
    public string MarkaAdi { get; set; } = string.Empty;
    public int MarkaId { get; set; }
    
    
}

