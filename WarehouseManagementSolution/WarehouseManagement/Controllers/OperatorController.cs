using System.Net;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;
using WebApplication1.Models.DTO.Response;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers;

[Authorize(Policy = IdentityData.AdminPolicyName + "," + IdentityData.BackOfficePolicyName)]
[Route("/api/[controller]/[action]")]
[Produces("application/json")]
public class OperatorController : ControllerBase
{
    private readonly IOperatorService _operatorService;

    public OperatorController(
        IOperatorService operatorService)
    {
        _operatorService = operatorService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(OperatorResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromBody] int? id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        if(id == null)
            return BadRequest("Id is null!");
        
        OperatorResponseDto operatorResponse = await _operatorService.Get(id.GetValueOrDefault());

        return Ok(operatorResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromBody] OperatorRequestDto operatorRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        if (operatorRequestDto.WarehouseId != null)
            return BadRequest("Warehouse cannot be assigned on creation!");

        await _operatorService.Create(operatorRequestDto);

        return Ok("Operator created!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update([FromBody] OperatorRequestDto operatorRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        await _operatorService.Update(operatorRequestDto);

        return Ok("Operator updated!");
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

        await _operatorService.Delete(id.GetValueOrDefault());

        return Ok("Operator deleted!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(List<OperatorResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        List<OperatorResponseDto> operators = await _operatorService.GetAll();

        return Ok(operators);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Validate([FromBody] UserOperatorRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        if(request.OperatorId == null || request.BackOfficeUserId == null)
            return BadRequest("Id is null!");

        await _operatorService.Validate(request.OperatorId.GetValueOrDefault(), request.BackOfficeUserId.GetValueOrDefault());

        return Ok("Operator validated!");
    }


}