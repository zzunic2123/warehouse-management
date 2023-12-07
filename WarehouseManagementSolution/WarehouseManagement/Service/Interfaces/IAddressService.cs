using WebApplication1.Models.DTO;
using WebApplication1.Models.Requests;

namespace WebApplication1.Service.Interfaces;

public interface IAddressService
{
    public Task Create(AddressRequestDto addressRequestDto);
    Task Update(AddressRequestDto addressRequestDto);
    Task<AddressResponseDto> Get(int id);
    Task Delete(int id);
}