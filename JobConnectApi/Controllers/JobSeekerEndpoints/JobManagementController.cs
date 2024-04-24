using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Route("/jobs")]
public class JobManagementController(
    DatabaseContext databaseContext,
    IJobService jobService,
    UserManager<JobSeeker> userManager,
    IJobSeekerService jobSeekerService,
    IProposalService proposalService) : ControllerBase
{
    [HttpGet("active")]
    public List<Job> GetActiveJobs()
    {
        List<Job> jobs = jobService.GetActiveJobs();
        return jobs;
    }

    [HttpGet("{jobId}")]
    public async Task<Job> GetJobDetails([FromRoute] string jobId)
    {
        Job job = await jobService.GetJobById(jobId);
        return job;
    }

    [HttpGet("{userId}/saved")]
    public async Task<List<Job>?> GetSavedJobs()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId == null) return null;
        var user = await userManager.FindByIdAsync(userId);
        if (user is { SavedJobs: { } jobs }) return jobs.ToList();

        return null;
    }


    [HttpPost("/{jobId}/save")]
    public async Task<ErrorOr<Updated>> SaveJob([FromRoute] string jobId)
    {
        string? userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            return await jobSeekerService.AddToSavedJobs(userId, jobId);
        }

        return Error.Unauthorized();
    }


    // POST /jobs/{jobId}/apply: Apply for a job (submit proposal with attachments).
    [HttpPost("/{jobId}/submit")]
    public async Task<ErrorOr<Created>> SubmitProposal(SubmitProposalDto proposalDto)
    {
        if (proposalDto.Cv.Length == 0)
        {
            return Error.Validation(description: "CV file is required");
        }
        string? userId = User.Claims.FirstOrDefault()?.Value;
        return userId != null ? await jobSeekerService.SubmitProposal(userId, proposalDto) : Error.Unauthorized();
    }
}