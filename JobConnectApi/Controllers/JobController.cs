using JobConnectApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    [HttpGet("/admin")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public IActionResult PostJob()
    {
        return Ok("You are allowed if you are admin only!");
    }
  
    
    [HttpGet("/emp")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Employer")]
    public IActionResult GetJob()
    {
        return Ok("You are allowed if you are Employer only!");
    }
    
}