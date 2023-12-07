using Domain.Models.DomainModels;
using WebApplication1.Models.Requests;

namespace WebApplication1.Service.Interfaces;

public interface IUserService
{
    Task<string> Login(UserLoginRequestDto requestDto);
    Task<string> Register(UserRegisterRequestDto requestDto);
    Task<string> RefreshToken();
    Task<string> AssignRole(AssignRoleRequestDto requestDto);
    Task<User> Get(int id);
}