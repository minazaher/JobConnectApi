using JobConnectApi.Controllers.JobSeekerEndpoints;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IProposalService
{
    Task<Proposal> SaveProposal(SubmitProposalDto submitProposalDto, String userId);
    Task<List<Proposal>> GetByJobId(string jobId);
    Task<Proposal> UpdateProposalStatus(string proposalId, ProposalStatus status, string employerId);
    Task<Proposal?> GetProposalById(string proposalId);
}