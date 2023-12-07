using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Requests;

namespace WebApplication1.Mappers;

public static class AddressMapper
{
    
    public static AddressResponseDto ToDto(this Address? address)
    {
        AddressResponseDto responseDto = new AddressResponseDto();
        
        responseDto.Id = address.Id;
        responseDto.Street = address.Street;
        responseDto.City = address.City;
        responseDto.ZipCode = address.ZipCode;

        return responseDto;
    }

    public static Address? ToDomain(this AddressRequestDto addressRequestDto)
    {
        Address? address = new Address();
        
        address.Id = addressRequestDto.Id;
        address.Street = addressRequestDto.Street;
        address.City = addressRequestDto.City;
        address.ZipCode = addressRequestDto.ZipCode;
        
        return address;
    }
    
}