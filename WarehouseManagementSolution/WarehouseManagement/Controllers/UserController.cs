using System.Net;
using Domain.Models.DomainModels;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Requests;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers;

[Route("/api/[controller]/[action]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(
        IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        string token = await _userService.Login(requestDto);

        return Ok(token);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");

        return Ok(await _userService.Register(requestDto));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok(await _userService.RefreshToken());
    }
    
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto requestDto)
    {
        return Ok(await _userService.AssignRole(requestDto));
    }
    
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromBody] int? id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model is invalid!");
        
        if(id == null)
            return BadRequest("Id is null!");
        
        User user = await _userService.Get(id.GetValueOrDefault());

        return Ok(user);
    }
}