using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Route("user")]
public class JobManagementController: ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IJobService _jobService;

    public JobManagementController(DatabaseContext databaseContext, IJobService jobService)
    {
        _databaseContext = databaseContext;
        _jobService = jobService;
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
  
}