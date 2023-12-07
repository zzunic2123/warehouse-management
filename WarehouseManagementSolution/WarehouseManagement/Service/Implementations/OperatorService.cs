using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Mappers;
using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class OperatorService : IOperatorService
{
    private readonly DatabaseContext _context;
    private readonly IWarehouseService _warehouseService;

    public OperatorService(DatabaseContext context, IWarehouseService warehouseService)
    {
        _context = context;
        _warehouseService = warehouseService;
    }

    public async Task<OperatorResponseDto> Get(int id)
    {
        Operator? @operator = await _context.Operators
            .Include(o => o.Validation)
            .Include(o => o.Warehouse)
            .SingleOrDefaultAsync(o => o.Id == id);

        if (@operator == null)
            throw new Exception("Operator not found");
        
        return @operator.ToDto();
    }

    public async Task Create(OperatorRequestDto operatorRequestDto)
    {
        try
        {

            if (operatorRequestDto == null)
                throw new Exception("Operator is null");
            
            Operator @operator = operatorRequestDto.ToDomain();
            @operator.Id = null;
            @operator.Validation = null;
            @operator.WarehouseId = null;
            @operator.Deleted = false;

            _context.Operators.Add(@operator);
            
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            if (e.Message.Contains("duplicate key value violates unique constraint"))
                throw new Exception("Operator already exists");
            
            throw;
        }
    }

    public async Task Update(OperatorRequestDto operatorRequestDto)
    {
        Operator? @operator = await _context.Operators.SingleOrDefaultAsync(o => o.Id == operatorRequestDto.Id);
        
        if(@operator == null)
            throw new Exception("Operator not found");
        
        if(@operator.Validation == null && @operator.WarehouseId != null)
            throw new Exception("Operator cannot be assigned to a warehouse without validation");
        
        @operator.Name = operatorRequestDto.Name;
        @operator.WarehouseId = operatorRequestDto.WarehouseId;

        await _context.SaveChangesAsync();

    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OperatorResponseDto>> GetAll()
    {
        IList<Operator> list = await _context.Operators
            .Include(o => o.Validation)
            .Include(o => o.Warehouse)
            .ToListAsync();
        
        if(list == null)
            throw new Exception("No operators found");
        
        return list.Select(x => x.ToDto()).ToList();
    }

    public async Task Validate(int operatorId, int userId)
    {
        Operator? @operator = await _context.Operators.SingleOrDefaultAsync(o => o.Id == operatorId);
        
        if(@operator == null)
            throw new Exception("Operator not found");
        
        if(@operator.Validation != null)
            throw new Exception("Operator already validated");
        
        OperatorValidation? validation = new OperatorValidation();
        validation.OperatorId = operatorId;
        validation.BackOfficeUserId = userId;
        validation.WarehouseId = @operator.WarehouseId;
        validation.CreatedAt = DateTimeOffset.UtcNow;
        
        @operator.Validation = validation;

        _context.OperatorValidations.Add(validation);
        await _context.SaveChangesAsync();
        
        
    }
    
    public async Task<Operator> GetOperatorByEmail(string email)
    {
        Operator? @operator = await _context.Operators.SingleOrDefaultAsync(o => o.Email == email);
        
        if(@operator == null)
            throw new Exception("Operator not found");

        return @operator;
    }
}