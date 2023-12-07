using System.Net;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Logging;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers;

[Authorize(Policy = IdentityData.AdminPolicyName + "," + IdentityData.BackOfficePolicyName + "," + IdentityData.OperatorPolicyName)]
[Route("/api/[controller]/[action]")]
[Produces("application/json")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogging _logger;

    public WarehouseController(
        IWarehouseService warehouseService,
        ILogging logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [ProducesResponseType(typeof(IList<WarehouseResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IList<WarehouseResponseDto>> GetAll()
    {
        return await _warehouseService.GetAll();
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    [ProducesResponseType(typeof(WarehouseResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<WarehouseResponseDto> GetById([FromBody] int? id)
    {
        if (id == null)
            throw new Exception("Id is null!");
        
        return await _warehouseService.GetById(id.GetValueOrDefault());
    }

    [HttpPost]
    [ProducesResponseType(typeof(WarehouseResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<WarehouseResponseDto> GetByName([FromBody] StringIdRequest request)
    {
        return await _warehouseService.GetByName(request.Id);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromBody] WarehouseRequestDto warehouseRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _warehouseService.Create(warehouseRequestDto);

        return Ok("Warehouse created!");
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update([FromBody] WarehouseRequestDto warehouseRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _warehouseService.Update(warehouseRequestDto);

        return Ok("Warehouse updated!");
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _warehouseService.Delete(id);

        return Ok("Warehouse deleted!");
    }
}