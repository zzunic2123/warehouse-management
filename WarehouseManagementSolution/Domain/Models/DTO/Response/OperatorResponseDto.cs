using WebApplication1.Models.DomainModels;
using WebApplication1.Models.Requests;

namespace WebApplication1.Models.DTO.Response;

public class OperatorResponseDto
{
    public int? Id { get; set; }
    
    public string Name { get; set; }
    
    public OperatorValidation? Validation { get; set; }
    
}