using JobConnectApi.Controllers.JobSeekerEndpoints;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IProposalService
{
    Task<Proposal> SaveProposal(SubmitProposalDto submitProposalDto, String userId);
}