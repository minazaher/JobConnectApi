using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace JobConnectApi.Controllers.AdminEndpoints;

[Route("admin/employers")]
[ApiController]
[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
public class AdminUserManagementController(IEmployerService employerService) : ControllerBase
{
    
    // GET /admin/employers: Get a list of all employers.
    [HttpGet]
    public async Task<IActionResult> GetAllEmployers()
    {
        var employers = await employerService.GetAllEmployers();
        if (!employers.Any())
        {
            return NotFound("No Employers found");
        }

        return Ok(employers);
    }

    // GET /admin/employers/{employerId}: Get details of a specific employer.
    [HttpGet("{employerId}")]
    public async Task<IActionResult> GetEmployerById([FromRoute] string employerId)
    {
        var employer = await employerService.GetEmployerById(employerId);
        return Ok(employer);
    }

    // POST /admin/employers: Create a new employer 
    [HttpPost]
    public async Task<IActionResult> AddEmployer([FromBody] RegisterRequest registerRequest)
    {
        Employer employer = await employerService.AddEmployer(registerRequest);
        return Ok(employer);
    }

    // DELETE /admin/employers/{employerId}: Delete an employer.
    [HttpDelete("{employerId}")]
    public async Task<IActionResult> DeleteEmployer([FromRoute] string employerId)
    {
        var isRemoved = await employerService.DeleteEmployerById(employerId);
        return isRemoved ? Ok("Employer Deleted") : Problem("Cannot delete the employer");
    }
}