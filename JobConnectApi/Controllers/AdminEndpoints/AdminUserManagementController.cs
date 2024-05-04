using AutoMapper;
using ErrorOr;
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
public class AdminUserManagementController(IEmployerService employerService, IMapper mapper, UserService userService) : ControllerBase
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

        var employerResponses = employers.Select(mapper.Map<EmployerDto>).ToList();
        return Ok(employerResponses);
    }

    // GET /admin/employers/{employerId}: Get details of a specific employer.
    [HttpGet("{employerId}")]
    public async Task<IActionResult> GetEmployerById([FromRoute] string employerId)
    {
        try
        {
            var employer = await employerService.GetEmployerById(employerId);

            var employerResponse = mapper.Map<EmployerDto>(employer);
            return Ok(employerResponse);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    // POST /admin/employers: Create a new employer 
    [HttpPost]
    public async Task<ErrorOr<Created>> AddEmployer([FromBody] RegisterRequest registerRequest)
    {
        return await userService.Register(registerRequest);
        // try
        // {
        //     var employer = await employerService.AddEmployer(registerRequest) == Result.Created;
        //     return employer ? CreatedAtAction(actionName: nameof(AddEmployer), value: registerRequest) : Problem("error saving the employer");
        // }
        // catch (Exception e)
        // {
        //     return Problem(e.Message);
        // }
    }

    // DELETE /admin/employers/{employerId}: Delete an employer.
    [HttpDelete("{employerId}")]
    public async Task<IActionResult> DeleteEmployer([FromRoute] string employerId)
    {
        var isRemoved = await employerService.DeleteEmployerById(employerId);
        return isRemoved ? Ok("Employer Deleted") : Problem("Cannot delete the employer");
    }
    
    [HttpPut("{employerId}")]
    public async Task<IActionResult> UpdateEmployer([FromRoute] string employerId,[FromBody] RegisterRequest registerRequest)
    {
        var isUpdated = await employerService.UpdateEmployer(employerId, registerRequest);
        return isUpdated ? Ok("Employer Updated") : Problem("Cannot Update the employer");
    }
}