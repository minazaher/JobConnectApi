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
[Route("employer/jobs")]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobManagementController(
    IJobService jobService)
    : ErrorController
{
    [HttpPost("add")]
    public async Task<IActionResult> PostJob([FromBody] JobRequest j)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        Console.WriteLine(employerId);
        if (employerId != null)
        {
            var result = await jobService.CreateJob(j, employerId);
            return result.Match(_ => CreatedAtPostJob(j),
                Problem);
        }

        return Unauthorized();
    }

    [HttpGet]
    public IActionResult GetEmployerJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        Console.WriteLine("user id is "+ userId);
        var jobs = jobService.FindByEmployerId(userId!);
        return jobs.Match(Ok, Problem);
    }


    private CreatedAtActionResult CreatedAtPostJob(JobRequest j)
    {
        return CreatedAtAction(
            actionName: nameof(PostJob),
            value: j);
    }
}