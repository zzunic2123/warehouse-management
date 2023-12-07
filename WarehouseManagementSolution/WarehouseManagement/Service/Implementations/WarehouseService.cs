using Domain.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Mappers;
using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class WarehouseService : IWarehouseService
{
    private readonly DatabaseContext _context;
    private readonly IUserContextService _userContextService;
    
    public WarehouseService(DatabaseContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContextService = userContextService;
    }
    
    public async Task<IList<WarehouseResponseDto>> GetAll()
    {
       IList<Warehouse> list = await _context.Warehouses.
           Include(w => w.Operator)
           .Include(w => w.Address)
           .ToListAsync();
       
       UserContextModel userContext = _userContextService.GetUserDetails();
       
       return list.Select(x => x.ToDto()).ToList();
    }

    public async Task<WarehouseResponseDto> GetById(int id)
    {

        Warehouse? warehouse = await _context.Warehouses.
            Include(w => w.Operator)
            .Include(w => w.Address)
            .SingleOrDefaultAsync(w => w.Id == id);
        
        if(warehouse == null)
            throw new Exception("Warehouse not found");
        
        return warehouse.ToDto();
    }

    public async Task<WarehouseResponseDto> GetByName(string requestId)
    {
        Warehouse? warehouse = await _context.Warehouses.SingleOrDefaultAsync(w => w.Name == requestId);
        
        if (warehouse == null)
            throw new Exception("Warehouse not found");
        
        return warehouse.ToDto();
    }

    public async Task Create(WarehouseRequestDto warehouseRequestDto)
    {
        try
        {
            Warehouse warehouse = warehouseRequestDto.ToDomain();
            
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (e.Message.Contains("duplicate key value violates unique constraint"))
                throw new Exception("Warehouse already exists");
            
            throw;
        }
    }

    public async Task Update(WarehouseRequestDto warehouseRequestDto)
    {
        Warehouse warehouse = warehouseRequestDto.ToDomain();
        
        Warehouse? existing = await _context.Warehouses.SingleOrDefaultAsync(w => w.Id == warehouse.Id);
        
        if (existing == null)
            throw new Exception("Warehouse not found");
        
        existing.Name = warehouse.Name;
        existing.Capacity = warehouse.Capacity;
        existing.OperatorId = warehouse.OperatorId;
        existing.AddressId = warehouse.AddressId;
        
        await _context.SaveChangesAsync();
        
    }

    public async Task Delete(int id)
    {
        Warehouse? warehouse = await _context.Warehouses.SingleOrDefaultAsync(w => w.Id == id);
        
        if (warehouse == null)
            throw new Exception("Warehouse not found");
        
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
    }
}