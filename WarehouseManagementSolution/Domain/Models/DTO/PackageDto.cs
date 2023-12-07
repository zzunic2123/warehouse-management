using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO;

public class PackageDto
{
    public int? Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public int? ReservationId { get; set; }

   // public ReservedDto Reserved { get; set; }
}