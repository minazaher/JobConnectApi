using JobConnectApi.Database;

using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace JobConnectApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private IJobService _jobService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DatabaseContext _databaseContext;

    public JobController(IJobService jobService, UserManager<IdentityUser> userManager, DatabaseContext databaseContext)
    {
        _jobService = jobService;
        _userManager = userManager;
        _databaseContext = databaseContext;
    }

    [HttpPost("/postjob")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    public async void PostJob([FromBody]Job j)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            var user = await _userManager.FindByIdAsync(userId);
        }

        // Create DTO for job object body
        // set employer as retrieved from db
        // set admin null till accepted 
    }
}