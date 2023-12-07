using Domain.Models.DomainModels;
using WebApplication1.Models.DomainModels;

namespace WebApplication1.JWTProvider;

public interface IJwtProvider
{
    string GenerateToken(User user);
}