using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.ErrorService;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var registerResult = await _userService.Register(request); // Assuming this returns ErrorOr<Unit>

        return registerResult.Match(
            _ => CreatedAtAction(nameof(Register), request), // Success
            error =>
            {
                // Log the error for debugging
                error.ForEach(err => Console.WriteLine("errors from main" + err.Description));

                // Return an appropriate HTTP response based on the error
                return TranslateToHttpResponse(error);
            });
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


    private IActionResult TranslateToHttpResponse(List<Error> errors)
    {
        var firstError = errors[0];
        var errorsString = string.Join(Environment.NewLine, errors.Select(Selector));
        Console.WriteLine(errorsString);
        
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: errorsString);
    }

    private string Selector(Error error)
    {
        return error.ToString();
    }
}