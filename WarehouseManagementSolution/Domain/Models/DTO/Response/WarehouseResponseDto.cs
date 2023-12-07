namespace WebApplication1.Models.DTO.Response;

public class WarehouseResponseDto
{
    public int? Id { get; set; }
    
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    public AddressResponseDto? Address { get; set; }

    public OperatorResponseDto? Operator { get; set; }

}