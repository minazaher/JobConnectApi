using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace JobConnectApi.Controllers.AdminEndpoints;

[Route("admin/employers")]
[ApiController]
// [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
public class AdminUserManagementController : ControllerBase
{
    private readonly IDataRepository<Employer> _employerRepository;

    public AdminUserManagementController(IDataRepository<Employer> employerRepository)
    {
        _employerRepository = employerRepository;
    }

    // GET /admin/employers: Get a list of all employers.
    [HttpGet]
    public async Task<IActionResult> GetAllEmployers()
    {
        var employers = await _employerRepository.GetAllAsync();
        var enumerableEmployers = employers.ToList();
        if (!enumerableEmployers.Any())
        {
            return NotFound("Employers Not Found");
        }

        return Ok(enumerableEmployers);
    }


    // GET /admin/employers/{employerId}: Get details of a specific employer.
    [HttpGet("{employerId}")]
    public async Task<IActionResult> GetEmployerById([FromRoute] string employerId)
    {
        var employer = await _employerRepository.GetByIdAsync(employerId);
        return Ok(employer);
    }

    // POST /admin/employers: Create a new employer 
    [HttpPost]
    public async Task<IActionResult> GetEmployerById([FromBody] RegisterRequest registerRequest)
    {
        var employer = new Employer
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            CompanyName = registerRequest.Company,
            Industry = registerRequest.Industry
        };

        await _employerRepository.AddAsync(employer);
        await _employerRepository.Save();
        return Ok(employer);
    }
    
    // DELETE /admin/employers/{employerId}: Delete an employer.
    [HttpDelete("{employerId}")]
    public async Task<IActionResult> DeleteEmployer([FromRoute] string employerId)
    {
        var employer = await _employerRepository.GetByIdAsync(employerId);
        await _employerRepository.DeleteAsync(employer);
        var isRemoved = await _employerRepository.Save();
        return isRemoved  ? Ok("Employer Deleted") : Problem("Cannot delete the employer");
    }
}