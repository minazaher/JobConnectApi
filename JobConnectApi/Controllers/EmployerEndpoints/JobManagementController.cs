using System.Security.Claims;
using AutoMapper;
using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Route("employer/jobs")]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobManagementController(
    IJobService jobService,
    IMapper mapper)
    : ErrorController
{
    [HttpPost("add")]
    public async Task<IActionResult> PostJob([FromBody] JobRequest j)
    {
        // 1. Extract employer ID with null check
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (employerIdClaim == null)
        {
            return Unauthorized(); // Clearer error message for missing employer
        }

        var employerId = employerIdClaim.Value;

        // 2. Handle job creation and validation
        try
        {
            var createdJob = await jobService.CreateJob(j, employerId);
            if (createdJob == new Job()) // Check for null createdJob
            {
                return BadRequest("Job creation failed. Please check the request data."); // Specific error message
            }

            var jobResponse = mapper.Map<JobResponse>(createdJob);

            return CreatedAtAction(actionName: nameof(PostJob), value: jobResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); // Maintain for specific error handling
        }
        catch (Exception ex)
        {
            return Problem(
                $"An error occurred while creating the job. Please try again later.\n the error details is {ex.Message}"); // Generic message for user
        }
    }


    [HttpGet]
    public IActionResult GetEmployerJobs()
    {
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (employerIdClaim == null)
        {
            return Unauthorized(); // Clearer error message for missing employer
        }

        var jobs = jobService.FindByEmployerId(employerIdClaim.Value);
        return Ok(jobs);
    }
}