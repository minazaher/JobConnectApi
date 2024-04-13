using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.AdminEndpoints;

[ApiController]
[Route("/admin/jobs")]
[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
public class AdminJobManagementController(
    IJobService jobService,
    UserManager<IdentityUser> userManager,
    DatabaseContext databaseContext)
    : ControllerBase
{
    // POST /admin/jobs/accept?jobId={id}: Accept a job post 
    [HttpPost("/accept")]
    public async void AcceptJobPost([FromQuery] int jobId)
    {
        Job job = await jobService.GetJobById(jobId);
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            var user = await userManager.FindByIdAsync(userId);
            job.Admin = user;
            job.AdminId = userId;
            job.Status = JobStatus.Accepted;
        }

        await databaseContext.SaveChangesAsync();
    }
    
    // GET /admin/jobs: Get a list of all job posts.
    [HttpGet]
    public async Task<List<Job>> GetAllJobs()
    {
        List<Job> jobs = jobService.FindAll();
        return jobs;
    }
}