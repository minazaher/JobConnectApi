using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Route("/jobs/{jobId}/proposals")]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobApplicationsController(IProposalService proposalService) : ControllerBase
{
    // GET /jobs/{jobId}/proposals: Get submitted proposals for a job.
    [HttpGet]
    public async Task<List<Proposal>> GetByJobId(string jobId)
    {
        return await proposalService.GetByJobId(jobId);
    }

    [HttpPost("{proposalId}/accept")]
    public async Task<Proposal> AcceptProposal([FromRoute] string proposalId)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        return await proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Accepted, employerId!);
    }

    [HttpPost("{proposalId}/reject")]
    public async Task<Proposal> RejectProposal([FromRoute] string proposalId)
    {
        var employerId = User.Claims.FirstOrDefault()?.Value;
        return await proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Rejected, employerId!);
    }
}