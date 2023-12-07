using WebApplication1.Models.DTO;

namespace WebApplication1.Service.Interfaces;

public interface IPackageService
{
    Task<IList<PackageDto>> GetAllFromWarehouse(string requestId);
    Task<IList<PackageDto>> GetAll();
    Task Create(PackageDto packageDto, string requestId);
    Task Update(PackageDto packageDto, string requestId);
    Task Delete(PackageDto productDto, string requestId);
    Task DepositPackage(PackageDto packageDto);
}