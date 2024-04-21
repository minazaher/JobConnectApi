using JobConnectApi.Controllers.JobSeekerEndpoints;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IProposalService
{
    Task<Proposal> SaveProposal(SubmitProposalDto submitProposalDto, String userId);
    Task<List<Proposal>> GetByJobId(int jobId);
    Task<Proposal> UpdateProposalStatus(string proposalId, ProposalStatus status);
}