using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Route("/jobs")]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobManagementController(
    IJobService jobService,
    UserManager<IdentityUser> userManager)
    : ControllerBase
{
    [HttpPost("add")]
    public async Task<ErrorOr<Created>> PostJob([FromBody] JobRequest j)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        if (employerId != null)
        {
            return await jobService.CreateJob(j, employerId);
        }

        return Error.Failure("Something wrong happened");
    }

    [HttpGet]
    public Task<List<Job>> GetEmployerJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;

        var jobs = jobService.FindByEmployerId(userId!);
        return jobs;
    }
}