using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Route("user")]
public class JobManagementController: ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IJobService _jobService;
    private readonly UserManager<IdentityUser> _userManager;

    public JobManagementController(DatabaseContext databaseContext, IJobService jobService, UserManager<IdentityUser> userManager)
    {
        _databaseContext = databaseContext;
        _jobService = jobService;
        _userManager = userManager;
    }

    [HttpGet("jobs")]
    public List<Job> GetActiveJobs()
    {
        List<Job> jobs = _jobService.GetActiveJobs();
        return jobs;
    }

    [HttpGet("jobs/{jobId}")]
    public async Task<Job> GetJobDetails([FromRoute] int jobId)
    {
        Job job = await _jobService.GetJobById(jobId);
        return job;
    }

    [HttpGet("saved")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<List<Job>?> GetSavedJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId == null) return null;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is JobSeeker jobSeeker)
        {
            Console.WriteLine("This user is Job Seeker");
            if (jobSeeker.SavedJobs != null) return jobSeeker.SavedJobs.ToList();
        }

        return null;
    }
}