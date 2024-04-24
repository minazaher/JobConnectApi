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
public class AdminJobManagementController(IJobService jobService, IAdminService adminService) : ControllerBase
{
    
    // POST /admin/jobs/accept?jobId={id}: Accept a job post 
    [HttpPost("/accept")]
    public void AcceptJobPost([FromQuery] int jobId) // TODO: Error Handling
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            adminService.SetJobAcceptedBy(jobId, userId);
        }
    }

    // POST /admin/jobs/accept?jobId={id}: Reject a job post 
    [HttpPost("/reject")]
    public void RejectJobPost([FromQuery] int jobId) // TODO: Error Handling
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            adminService.SetJobRejectedBy(jobId, userId);
        }
    }

    // GET /admin/jobs: Get a list of all job posts.
    [HttpGet]
    public List<Job> GetAllJobs() // TODO: Error Handling 
    {
        List<Job> jobs = jobService.FindAllJobs();
        return jobs;
    }

    // GET /admin/jobs/waiting Get details of a specific job post.
    [HttpGet("waiting")]
    public List<Job> GetWaitingList() // TODO: Error Handling 
    {
        List<Job> jobs = jobService.GetJobsWaitingList();
        return jobs;
    }

    // GET /admin/jobs/jobId Get details of a specific job post.
    [HttpGet("{jobId}")]
    public async Task<Job> FindJobById([FromRoute] int jobId) // TODO: Error Handling 
    {
        Job job = await jobService.GetJobById(jobId);
        return job;
    }
}