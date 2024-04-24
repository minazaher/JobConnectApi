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
    [HttpPost]
    public async void PostJob([FromBody] JobRequest j)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        if (employerId != null)
        {
            jobService.CreateJob(j, employerId);
        }
    }

    [HttpGet]
    public List<Job> GetEmployerJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;

        var jobs = jobService.FindByEmployerId(userId!);
        return jobs;
    }
}