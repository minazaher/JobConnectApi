using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Authorize(Roles = "JobSeeker", AuthenticationSchemes = "Bearer")]

[Route("/jobs")]
public class JobManagementController(
    DatabaseContext databaseContext,
    IJobService jobService,
    UserManager<IdentityUser> userManager,
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
        if (user is JobSeeker js && !js.SavedJobs.IsNullOrEmpty())
        {
            return js.SavedJobs.ToList();
        }

        return null;
    }


    [HttpPost("{jobId}/save")]
    public async Task<ErrorOr<Updated>> SaveJob([FromRoute] string jobId)
    {
        string? userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            return await jobSeekerService.AddToSavedJobs(userId, jobId);
        }

        return Error.Unauthorized();
    }


    [HttpPost("apply")] // Keep the route template for consistency
    public async Task<IActionResult> SubmitProposal([FromForm] SubmitProposalDto proposalDto)
    {
        if (proposalDto.Cv.Length == 0)
        {
            return Problem( "CV file is required");
        }
        Console.WriteLine("the sent jobId is" +   proposalDto.JobId);
        string? userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            await jobSeekerService.SubmitProposal(userId, proposalDto);
            return Ok("Saved");
        }

        return Problem("IDK what happened");
    }
}