using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Route("user")]
public class JobManagementController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IJobService _jobService;
    private readonly IProposalService _proposalService;
    private readonly UserManager<IdentityUser> _userManager;

    public JobManagementController(DatabaseContext databaseContext, IJobService jobService,
        UserManager<IdentityUser> userManager, IProposalService proposalService)
    {
        _databaseContext = databaseContext;
        _jobService = jobService;
        _userManager = userManager;
        _proposalService = proposalService;
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
            if (jobSeeker.SavedJobs != null) return jobSeeker.SavedJobs.ToList();
        }

        return null;
    }
    
    
    [HttpPost("jobs/save/{jobId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async void SaveJob([FromRoute] int jobId)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        Job job = await _jobService.GetJobById(jobId);
        if (userId == null) return ;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is JobSeeker jobSeeker)
        {
            if (jobSeeker.SavedJobs != null)
            {
                List<Job> jobs = jobSeeker.SavedJobs.ToList();
                jobs.Add(job);
                jobSeeker.SavedJobs = jobs;
                await _databaseContext.SaveChangesAsync();
            }
        }

    }

    [HttpPost]
    [Route("proposal/submit")]
    [Authorize(Roles = "JobSeeker", AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult?> SubmitProposal(SubmitProposalDto proposalDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.Claims.FirstOrDefault()?.Value;
        Console.WriteLine(userId);
        if (userId == null) return Created();

        if (proposalDto.Cv.Length == 0)
        {
            return BadRequest(new { message = "CV file is required." });
        }

        Proposal proposal = await _proposalService.SaveProposal(proposalDto, userId);
        
        return CreatedAtRoute(
            "GetProposal",
            new { proposalId = proposal.ProposalId },
            proposal); // Return created proposal resource
    }

  
}


