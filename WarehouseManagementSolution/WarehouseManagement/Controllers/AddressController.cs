using System.Net;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers;


[Authorize(Policy = IdentityData.AdminPolicyName + "," + IdentityData.BackOfficePolicyName)]
 [Route("/api/[controller]/[action]")]
[Produces("application/json")]
public class AddressController : ControllerBase
{
    
    private readonly IAddressService _addressService;

    public AddressController(
        IAddressService addressService)
    {
        _addressService = addressService;
    }

    
    [HttpPost]
    [ProducesResponseType(typeof(AddressResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromBody] int? id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        if(id == null)
            return BadRequest("Id is null!");
        
        AddressResponseDto addressResponse = await _addressService.Get(id.GetValueOrDefault());

        return Ok(addressResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromBody] AddressRequestDto addressRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _addressService.Create(addressRequestDto);

        return Ok("Address created!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update([FromBody] AddressRequestDto addressRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _addressService.Update(addressRequestDto);

        return Ok("Address updated!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromBody] int? id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        if(id == null)
            return BadRequest("Id is null!");
        
        await _addressService.Delete(id.GetValueOrDefault());

        return Ok("Address deleted!");
    }
  
}