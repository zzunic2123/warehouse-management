using WebApplication1.Models.DTO.Response;

namespace WebApplication1.Models.DTO;

public class AddressResponseDto
{
    public int? Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}