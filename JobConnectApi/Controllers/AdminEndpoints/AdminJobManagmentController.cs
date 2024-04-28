using AutoMapper;
using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Mapper;
using JobConnectApi.Models;
using JobConnectApi.Services;
using JobConnectApi.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Controllers.AdminEndpoints;

[ApiController]
[Route("/admin/jobs")]
[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
public class g(IJobService jobService, IAdminService adminService, IMapper mapper)
    : ControllerBase
{
    // POST /admin/jobs/accept?jobId={id}: Accept a job post 
    [HttpPost("accept")]
    public async Task<IActionResult> AcceptJobPost([FromQuery] string jobId) 
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            return await adminService.SetJobAcceptedBy(jobId, userId) ? Ok() : Problem("Failed to update Job status");
        }

        return Unauthorized();
    }

    // POST /admin/jobs/reject?jobId={id}: Reject a job post 
    [HttpPost("reject")]
    public async Task<IActionResult> RejectJobPost([FromQuery] string jobId) 
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            return await adminService.SetJobRejectedBy(jobId, userId) ? Ok() : Problem("Failed to update Job status");
        }

        return Unauthorized();
    }

    // GET /admin/jobs: Get a list of all job posts.
    [HttpGet]
    public IActionResult GetAllJobs()
    {
        List<Job> jobs = jobService.FindAllJobs();
        if (jobs.IsNullOrEmpty())
        {
            return NotFound("No jobs found.");
        }
        var jobResponses = jobs.Select(mapper.Map<JobResponse>).ToList();

        return Ok(jobResponses);
    }

    // GET /admin/jobs/waiting Get details of a specific job post.
    [HttpGet("waiting")]
    public IActionResult GetWaitingList()
    {
        var jobs = jobService.GetJobsWaitingList();
        if (jobs.IsNullOrEmpty())
        {
            return NotFound("No jobs found.");
        }

        var jobResponses = jobs.Select(mapper.Map<JobResponse>).ToList();
        return Ok(jobResponses);
    }

    // GET /admin/jobs/jobId Get details of a specific job post.
    [HttpGet("{jobId}")]
    public async Task<IActionResult> FindJobById([FromRoute] string jobId) 
    {
        try
        {
            Job job = await jobService.GetJobById(jobId);
            var responseDto = mapper.Map<JobResponse>(job);

            return Ok(responseDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}