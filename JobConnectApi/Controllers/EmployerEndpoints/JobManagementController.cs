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
    UserManager<IdentityUser> userManager,
    UserService userService,
    DatabaseContext databaseContext)
    : ControllerBase
{
    [HttpPost]
    public async void PostJob([FromBody] JobRequest j)
    {
        Job job = new Job
        {
            JobTitle = j.JobTitle,
            JobDescription = j.JobDescription,
            JobType = j.JobType,
            JobId = j.JobId,
            Salray = j.Salray,
            PostDate = j.PostDate,
            Status = JobStatus.Pending,
            IsActive = true
        };

        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            var user = await userManager.FindByIdAsync(userId);
            // job.Employer = user;
            job.EmployerId = userId;
            job.AdminId = null;
            job.Admin = null;
        }

        jobService.CreateJob(job);
    }

    [HttpGet]
    public List<Job> GetEmployerJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;

        var jobs = jobService.FindByEmployerId(userId!);
        return jobs;
    }
}