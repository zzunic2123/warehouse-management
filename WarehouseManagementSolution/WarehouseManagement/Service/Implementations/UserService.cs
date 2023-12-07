using Azure;
using Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.JWTProvider;
using WebApplication1.Models.DomainModels;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;
using WebApplication1.Utils;

namespace WebApplication1.Service.Implementations;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserContextService _userContextService;
    
    public UserService(
        IJwtProvider jwtProvider,
        DatabaseContext context, IPasswordHasher passwordHasher,
        IHttpContextAccessor httpContextAccessor, 
        IUserContextService userContextService)
    {
        _jwtProvider = jwtProvider;
        _context = context;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
        _userContextService = userContextService;
    }
    
    public async Task<string> Login(UserLoginRequestDto requestDto)
    {
        if (!EmailValidator.IsValidEmail(requestDto.Email))
            throw new("Email is not valid");

        User? user = await _context.Users
            .Include(u => u.Roles)
            .SingleOrDefaultAsync(u => u.Email == requestDto.Email);
        
        if (user == null)
            throw new Exception("User not found");

        if (!_passwordHasher.VerifyPassword(requestDto.Password, user.Password))
            throw new Exception("Password is not valid");
        
        string token = _jwtProvider.GenerateToken(user);
        
        RefreshToken refreshToken = GenerateRefreshToken(user);
        await SetRefreshToken(user, refreshToken);

        return token;
    }


    public async Task<string> Register(UserRegisterRequestDto requestDto)
    {
        if (!EmailValidator.IsValidEmail(requestDto.Email))
            throw new("Email is not valid");
        
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == requestDto.Email);

        if (user != null)
            throw new("User with that email already exists");


        User newUser = new User();
        newUser.Name = $"{requestDto.FamilyName} {requestDto.GivenName}";
        newUser.Email = requestDto.Email;
        newUser.Password = _passwordHasher.Hash(requestDto.Password);
        newUser.PreferredUsername = requestDto.PreferredUsername;
        newUser.GivenName = requestDto.GivenName;
        newUser.FamilyName = requestDto.FamilyName;
        newUser.TenantId = requestDto.TenantId;
        
        Role? role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "USER");

        IEnumerable<Role> roles = new[] { role! };
        newUser.Roles = roles.ToList();

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        
        return _jwtProvider.GenerateToken(newUser);
    }

    public async Task<string> RefreshToken()
    {
        string refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["RefreshToken"];
        
        if (refreshToken == null)
            throw new Exception("Refresh token is not provided");
        
        User? user = await _context.Users
            .Include(u => u.RefreshToken)
            .SingleOrDefaultAsync(u => u.Id == _userContextService.GetUserDetails().Id);
        
        if (user == null)
            throw new Exception("User not found");
        
        if(user.RefreshToken.Token != refreshToken)
            throw new Exception("Refresh token is not valid");
        
        if(user.RefreshToken.ExpiresAt < DateTimeOffset.UtcNow)
            throw new Exception("Refresh token has expired");
        
        string token = _jwtProvider.GenerateToken(user);
        RefreshToken newRefreshToken = GenerateRefreshToken(user);
        await SetRefreshToken(user, newRefreshToken);
        
        return token;
    }

    public async Task<string> AssignRole(AssignRoleRequestDto requestDto)
    {
        User? user = _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == requestDto.UserId);
        
        if (user == null)
            throw new Exception("User not found");
        
        Role? role = _context.Roles.SingleOrDefault(r => r.Name == requestDto.RoleName);
        
        if (role == null)
            throw new Exception("Role not found");
        user.Roles.Clear();
        user.Roles.Add(role);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        return "Role assigned";
    }

    public async Task<User> Get(int id)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.RefreshToken)
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    private RefreshToken GenerateRefreshToken(User user)
    {
        RefreshToken refreshToken = new RefreshToken();
        refreshToken.Token = Guid.NewGuid().ToString();
        
        DateTimeOffset now = DateTimeOffset.UtcNow;
        refreshToken.ExpiresAt = now.AddDays(7);
        refreshToken.CreatedAt = now; 
        refreshToken.UserId = user.Id;
        refreshToken.User = user;

        return refreshToken;
    }
    private async Task SetRefreshToken(User user, RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExpiresAt
        };
        
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
        user.RefreshToken = refreshToken;
        _context.RefreshTokens.Add(refreshToken);
        
        await _context.SaveChangesAsync();

    }

}