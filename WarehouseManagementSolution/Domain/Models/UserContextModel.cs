namespace Domain.Models;

public class UserContextModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int TenantId { get; set; }
    public int WarehouseId { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}