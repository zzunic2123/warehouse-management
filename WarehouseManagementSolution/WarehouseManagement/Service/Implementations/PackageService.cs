using WebApplication1.Context;
using WebApplication1.Mappers;
using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class PackageService : IPackageService
{
    private readonly DatabaseContext _context;
    private readonly IUserContextService _userContextService;
    
    public PackageService(DatabaseContext context,
        IUserContextService userContextService)
    {
        _context = context;
        _userContextService = userContextService;
    }
    
    public async Task<IList<PackageDto>> GetAllFromWarehouse(string requestId)
    {
        throw new NotImplementedException();

    }

    public async Task<IList<PackageDto>> GetAll()
    {
        throw new NotImplementedException();

    }

    public async Task Create(PackageDto packageDto, string requestId)
    {
        try
        {
            Package package = packageDto.ToDomain();
            _context.Packages.Add(package);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (e.Message.Contains("duplicate key value violates unique constraint"))
                throw new Exception("Warehouse already exists");
            
            throw;
        }
    }

    public async Task Update(PackageDto packageDto, string requestId)
    {
        throw new NotImplementedException();

    }

    public async Task Delete(PackageDto packageDto, string requestId)
    {
        throw new NotImplementedException();

    }

    public Task DepositPackage(PackageDto packageDto)
    {
        throw new NotImplementedException();
    }
}