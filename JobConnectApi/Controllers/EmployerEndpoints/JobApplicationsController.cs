using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers.EmployerEndpoints;

[ApiController]
[Route("/jobs/{jobId}/proposals")]
[Authorize(Roles = "Employer", AuthenticationSchemes = "Bearer")]
public class JobApplicationsController
{
    private readonly IProposalService _proposalService;

    public JobApplicationsController(IProposalService proposalService)
    {
        _proposalService = proposalService;
    }
    
    // GET /jobs/{jobId}/proposals: Get submitted proposals for a job.
    [HttpGet]
    public async Task<List<Proposal>> GetByJobId(string jobId)
    {
        return await _proposalService.GetByJobId(jobId);
    }

    [HttpPost("{proposalId}/accept")]
    public async Task<Proposal> AcceptProposal([FromRoute] string proposalId)
    {
        return await _proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Accepted);
    }
    
    [HttpPost("{proposalId}/reject")]
    public async Task<Proposal> RejectProposal([FromRoute] string proposalId)
    {
        return await _proposalService.UpdateProposalStatus(proposalId, ProposalStatus.Rejected);
    }
        
    
}