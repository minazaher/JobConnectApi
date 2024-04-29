using ErrorOr;
using JobConnectApi.Models;

namespace JobConnectApi.Services.UserServices;

public interface IJobSeekerService
{
    Task<ErrorOr<Updated>> AddToSavedJobs(string userId ,string jobId);
    Task<bool> SubmitProposal(string userId ,SubmitProposalDto proposalDto);
    
    // Task<List<Chat>> GetJobSeekerChats(string id);

}