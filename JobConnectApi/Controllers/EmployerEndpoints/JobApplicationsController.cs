using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobApplicationsController(IProposalService proposalService) : ControllerBase
{
    private static readonly string DirectoryPath = "D:\\Projects\\.Net\\JobConnectApi\\JobConnectApi\\";
    // GET /jobs/{jobId}/proposals: Get submitted proposals for a job.
    [HttpGet]
    [Route("/jobs/{jobId}/proposals")]
    public async Task<List<Proposal>> GetByJobId(string jobId)
    {
        return await proposalService.GetByJobId(jobId);
    }

    [HttpGet]
    [Route("/jobs/proposals/{proposalId}")]
    public async Task<IActionResult> GetProposalById([FromRoute] string proposalId)
    {
        var proposal = await proposalService.GetProposalById(proposalId);
        return proposal != null ? Ok(proposal) : NotFound("No Proposal Found with this ID");
    }

    [HttpGet]
    [Route("/jobs/proposals/{proposalId}/CV")]
    public async Task<IActionResult> GetProposalCv([FromRoute] string proposalId)
    {
        var proposal = await proposalService.GetProposalById(proposalId);
        var attachmentPath = proposal?.AttachmentPath;
        if (string.IsNullOrEmpty(attachmentPath))
        {
            return BadRequest("Missing attachment path");
        }

        // Replace "uploads" with your actual uploads directory path
        // attachmentPath = attachmentPath.Replace(@"\\uploads\\", @"\\wwwroot\\uploads\\");
        var filePath = Path.Combine("D:\\Projects\\.Net\\JobConnectApi\\JobConnectApi\\wwwroot\\", attachmentPath);

        Console.WriteLine("path is " + filePath);
        
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File not found");
        }

        var stream = System.IO.File.OpenRead(filePath);
        return File(stream, "application/pdf", Path.GetFileName(filePath));
    }


    [HttpPost("/jobs/proposals/{proposalId}/accept")]
    public async Task<Proposal> AcceptProposal([FromRoute] string proposalId)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        return await proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Accepted, employerId!);
    }

    [HttpPost("/jobs/proposals/{proposalId}/reject")]
    public async Task<Proposal> RejectProposal([FromRoute] string proposalId)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        return await proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Rejected, employerId!);
    }
}