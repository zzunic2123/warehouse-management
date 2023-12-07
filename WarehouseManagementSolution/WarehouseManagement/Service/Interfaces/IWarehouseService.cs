using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;

namespace WebApplication1.Service.Interfaces;

public interface IWarehouseService
{
    Task<IList<WarehouseResponseDto>> GetAll();
    Task<WarehouseResponseDto> GetById(int id);
    Task<WarehouseResponseDto> GetByName(string requestId);
    Task Create(WarehouseRequestDto warehouseRequestDto);
    Task Update(WarehouseRequestDto warehouseRequestDto);
    Task Delete(int id);
}