using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;

namespace WebApplication1.Mappers;

public static class OperatorMapper
{
    
    public static OperatorResponseDto ToDto(this Operator @operator)
    {
        OperatorResponseDto responseDto = new OperatorResponseDto();
        
        
        responseDto.Id = @operator.Id;
        responseDto.Name = @operator.Name;
        responseDto.Validation = @operator.Validation;
        //dto.Warehouse = @operator.Warehouse?.ToDto();
        

        return responseDto;
    }
    
    public static Operator ToDomain(this OperatorRequestDto requestDto)
    {
        Operator @operator = new Operator();
        
        @operator.Id = requestDto.Id;
        @operator.Name = requestDto.Name;
        @operator.ValidationId = requestDto.OperatorValidationId; 
        @operator.WarehouseId = requestDto.WarehouseId;
        //@operator.Warehouse = dto.Warehouse?.ToDomain();
        
        return @operator;
    }

    public static UserLogin ToDto(this UserLoginRequestDto requestDto)
    {
        UserLogin login = new UserLogin();

        login.Email = requestDto.Email;
        login.Password = requestDto.Password;

        return login;
    }

    public static UserLoginRequestDto ToDomain(this UserLogin login)
    {
        UserLoginRequestDto requestDto = new UserLoginRequestDto();

        requestDto.Email = login.Email;
        requestDto.Password = login.Password;

        return requestDto;
    }
    
}