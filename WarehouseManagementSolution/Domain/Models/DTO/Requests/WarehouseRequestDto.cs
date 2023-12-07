using WebApplication1.Models.DTO;

namespace WebApplication1.Models.Requests;

public class WarehouseRequestDto
{
    public int? Id { get; set; }
    
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    public int? OperatorId { get; set; }
    
    public int? AddressId { get; set; }
    
}