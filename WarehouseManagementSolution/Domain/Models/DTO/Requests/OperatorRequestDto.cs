namespace WebApplication1.Models.Requests;

public class OperatorRequestDto
{
    public int? Id { get; set; }
    
    public string Name { get; set; }
    
    public int? OperatorValidationId { get; set; }
    
    public int? WarehouseId { get; set; }
}