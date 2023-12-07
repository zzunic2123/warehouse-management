using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.DomainModels;

[Table("Role")]
public class Role
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }

    public IEnumerable<User> Users { get; set; } = new List<User>();
}