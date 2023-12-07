using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DomainModels;

public class UserLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}