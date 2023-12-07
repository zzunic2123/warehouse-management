using Domain.Models;

namespace WebApplication1.Service.Interfaces;

public interface IUserContextService
{
    UserContextModel GetUserDetails();
}