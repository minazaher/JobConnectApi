using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = JobConnectApi.DTOs.RegisterRequest;

namespace JobConnectApi.Controllers;

[Controller]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<CreatedAtActionResult> Register([FromBody] RegisterRequest request)
    {
        await _userService.Register(request);
        return CreatedAtAction(actionName: nameof(Register), value: request);
    }
    
    /*
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _userService.Login(loginRequest);
        if (response.Successful)
        {
            return Ok(response); 
        }

        return Unauthorized(response.Message);
    }
    */
}