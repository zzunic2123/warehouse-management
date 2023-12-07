using System.Net;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers;

[Authorize(Policy = IdentityData.AdminPolicyName + "," + IdentityData.BackOfficePolicyName + "," + IdentityData.OperatorPolicyName)]
[Route("/api/[controller]/[action]")]
[Produces("application/json")]
public class PackageController : Controller
{
    private readonly IPackageService _packageService;
    
    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(IList<PackageDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IList<PackageDto>> GetAll()
    {
        return await _packageService.GetAll();
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(IList<PackageDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IList<PackageDto>> GetAllFromWarehouse([FromBody] StringIdRequest request)
    {
        return await _packageService.GetAllFromWarehouse(request.Id);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> AddProductInWarehouse([FromBody] PackageDto packageDto)
    {
        StringIdRequest request = new StringIdRequest();
        request.Id = null;
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        await _packageService.Create(packageDto, request.Id);

        return Ok("Product added in warehouse!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateProductInWarehouse([FromBody] PackageDto packageDto, StringIdRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        await _packageService.Update(packageDto, request.Id);
        
        return Ok("Product updated in warehouse!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteProductInWarehouse([FromBody] PackageDto packageDto, StringIdRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        await _packageService.Delete(packageDto,request.Id);
        
        return Ok("Product deleted from warehouse!");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DepositPackage([FromBody] PackageDto packageDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        await _packageService.DepositPackage(packageDto);
        
        return Ok("Package deposited!");
    }
}