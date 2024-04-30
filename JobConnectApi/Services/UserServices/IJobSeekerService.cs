using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Services.UserServices;

public interface IJobSeekerService
{
    Task<ErrorOr<Updated>> AddToSavedJobs(string userId ,string jobId);
    Task<bool> SubmitProposal(string userId ,SubmitProposalDto proposalDto);
    Task<List<JobResponse>?> GetUserSavedJobs(string userId);
    Task<ErrorOr<Updated>> RemoveFromSavedJobs(string userId, string jobId);
}