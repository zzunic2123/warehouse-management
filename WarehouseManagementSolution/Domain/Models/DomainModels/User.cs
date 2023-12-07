using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.DomainModels;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PreferredUsername { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string FamilyName { get; set; } = null!;
    public bool EmailVerified { get; set; }
    public int? TenantId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public IList<Role> Roles { get; set; } = new List<Role>();
    public RefreshToken RefreshToken { get; set; }
}