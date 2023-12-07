using System.Security.Claims;
using Domain.Models;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class UserContextService: IUserContextService
{
    
    private readonly HttpContext _context;
 
    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _context = httpContextAccessor.HttpContext;
    }
 
    public UserContextModel GetUserDetails()
    {
        var userContext = new UserContextModel();
        string? id = _context.Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? email = _context.Request.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        string? tenantId = _context.Request.HttpContext.User.FindFirst("TenantId")?.Value;
        string? warehouseId = _context.Request.HttpContext.User.FindFirst("WarehouseId")?.Value;
        string? roles = _context.Request.HttpContext.User.FindFirst("Roles")?.Value;
        
        userContext.Id = int.Parse(id ?? string.Empty);
        userContext.Email = email;
        userContext.TenantId = int.TryParse(tenantId, out int tenantIdValue) ? tenantIdValue : 0;
        userContext.WarehouseId = int.TryParse(warehouseId, out int warehouseIdValue) ? warehouseIdValue : 0;
        userContext.Roles = roles?.Split(',').ToList() ?? new List<string>();
        
        return userContext;
    }
}