using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.JobSeekerEndpoints;

[ApiController]
[Route("jobs")]
public class JobManagementController: ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IJobService _jobService;

    public JobManagementController(DatabaseContext databaseContext, IJobService jobService)
    {
        _databaseContext = databaseContext;
        _jobService = jobService;
    }

    [HttpGet("user")]
    public List<Job> GetActiveJobs()
    {
        List<Job> jobs = _jobService.GetActiveJobs();
        return jobs;
    } 
  
}