using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.DomainModels;

[Table("RefreshToken")]
public class RefreshToken
{
    public int Id { get; set; }
    
    public string Token { get; set; } = null!;
    
    public DateTimeOffset ExpiresAt { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
}