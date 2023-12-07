using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;

namespace WebApplication1.Service.Interfaces;

public interface IOperatorService
{
    Task<OperatorResponseDto> Get(int id);
    Task Create(OperatorRequestDto operatorRequestDto);
    Task Update(OperatorRequestDto operatorRequestDto);
    Task Delete(int id);
    Task<List<OperatorResponseDto>> GetAll();
    Task Validate(int operatorId, int userId);
    Task<Operator> GetOperatorByEmail(string email);
}