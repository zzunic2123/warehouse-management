using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests;

public class AssignRoleRequestDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string RoleName { get; set; } = null!;
}