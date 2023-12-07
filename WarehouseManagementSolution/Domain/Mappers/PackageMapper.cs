using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappers;

public static class PackageMapper
{
    
    public static PackageDto ToDto(this Package package)
    {
        PackageDto packageDto = new PackageDto();
        
        packageDto.Id = package.Id;
        packageDto.Name = package.Name;
        packageDto.Description = package.Description;
        packageDto.Quantity = package.Quantity;
       // packageDto.Reserved = package.Reserved.ToDto();
        
        return packageDto;
    }
    
    public static Package ToDomain(this PackageDto packageDto)
    {
        Package package = new Package();
        
        package.Id = packageDto.Id;
        package.Name = packageDto.Name;
        package.Description = packageDto.Description;
        package.Quantity = packageDto.Quantity;
       // package.Reserved = packageDto.Reserved.ToDomain();
        
        return package;
    }
}