using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = JobConnectApi.DTOs.RegisterRequest;

namespace JobConnectApi.Controllers;

[Controller]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        
        var user = new User(request.Email, request.Password, request.UserType, request.FirstName, request.LastName);
        userService.Register(user);
        return CreatedAtAction(actionName: nameof(Register), value: user);
    }
    
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
    
}