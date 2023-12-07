using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Mappers;
using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations;

public class AddressService : IAddressService
{
    private readonly DatabaseContext _context;

    public AddressService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task Create(AddressRequestDto addressRequestDto)
    {
        try
        {
            if (addressRequestDto == null)
                throw new Exception("Address is null");
        
            Address address = addressRequestDto.ToDomain();
            address.Id = null;
            address.Deleted = false;
            
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (e.Message.Contains("duplicate key value violates unique constraint"))
                throw new Exception("Address already exists");
            
            throw;
        }
        
    }

    public async Task Update(AddressRequestDto addressRequestDto)
    {
        Address address = addressRequestDto.ToDomain();
        
        Address? existing = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == address.Id && x.Deleted != true);
        
        if (existing == null)
            throw new Exception("Address not found");
        
        existing.Street = address.Street;
        existing.City = address.City;
        existing.ZipCode = address.ZipCode;
        
        await _context.SaveChangesAsync();
    }

    public async Task<AddressResponseDto> Get(int id)
    {
        Address? address = await _context.Addresses
            .FirstOrDefaultAsync(x => x.Id == id && x.Deleted != true);
        
        if (address == null)
            throw new Exception("Address not found");
        
        return address.ToDto();
    }

    public async Task Delete(int id)
    {
        Address? address = _context.Addresses.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        
        if (address == null)
            throw new Exception("Address not found");
        
        address.Deleted = true;
        
        await _context.SaveChangesAsync();
    }
}