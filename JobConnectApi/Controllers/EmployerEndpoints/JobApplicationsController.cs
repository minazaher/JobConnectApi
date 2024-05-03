using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobApplicationsController(IProposalService proposalService) : ControllerBase
{
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