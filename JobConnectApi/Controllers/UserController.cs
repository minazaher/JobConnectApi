using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.ErrorService;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Console.Write(request);

        var registerResult = await _userService.Register(request); // Assuming this returns ErrorOr<Unit>
        return registerResult.Match(
            _ => CreatedAtAction(nameof(Register), request), // Success
            TranslateToHttpResponse);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _userService.Login(loginRequest);
        if (response.Successful)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    

    private IActionResult TranslateToHttpResponse(List<Error> errors)
    {
        var firstError = errors[0];
        Console.WriteLine("error returned is : " + errors[0]);
        
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: firstError.Description);
    }
    
}