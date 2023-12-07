using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;

namespace WebApplication1.Mappers;

public static class WarehouseMapper
{

    /*
    public static WarehouseRequestDto ToDto(this Warehouse warehouse)
    {
        WarehouseRequestDto requestDto = new WarehouseRequestDto();

        //requestDto.Id = warehouse.Id;
        requestDto.Name = warehouse.Name;
        requestDto.Capacity = warehouse.Capacity;
        requestDto.OperatorId = warehouse.OperatorId;
        requestDto.AddressId = warehouse.OperatorId;

        return requestDto;
    }*/

    public static WarehouseResponseDto ToDto(this Warehouse warehouse)
    {
        WarehouseResponseDto responseDto = new WarehouseResponseDto();

        responseDto.Id = warehouse.Id;
        responseDto.Name = warehouse.Name;
        responseDto.Capacity = warehouse.Capacity;
        responseDto.Address = warehouse.Address?.ToDto();
        responseDto.Operator = warehouse.Operator?.ToDto();
        
        return responseDto;
    }
    
    public static Warehouse ToDomain(this WarehouseRequestDto requestDto)
    {
        Warehouse warehouse = new Warehouse();
        
        warehouse.Id = requestDto.Id;
        warehouse.Name = requestDto.Name;
        warehouse.Capacity = requestDto.Capacity;
        warehouse.OperatorId = requestDto.OperatorId;
        warehouse.AddressId = requestDto.AddressId;

        return warehouse;
    }
    
}