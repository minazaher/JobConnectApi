using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Services.UserServices;

public class JobSeekerService(
    IJobService jobService,
    IDataRepository<JobSeeker> userRepository,
    IProposalService proposalService) : IJobSeekerService
{
    public async Task<ErrorOr<Updated>> AddToSavedJobs(string userId, string jobId)
    {
        Job job = await jobService.GetJobById(jobId);
        var jobSeeker = await userRepository.GetByIdAsync(userId);

        if (jobSeeker?.SavedJobs != null)
        {
            List<Job> jobs = jobSeeker.SavedJobs.ToList();
            jobs.Add(job);
            jobSeeker.SavedJobs = jobs;
            var saved = await userRepository.Save();
            return saved ? Result.Updated : Error.Failure(description: "Error In Saving Job");
        }

        return Error.NotFound(description: "Saved Jobs Not Found");
    }

    public async Task<bool> SubmitProposal(string userId, SubmitProposalDto proposalDto)
    {
        try
        {
            Proposal proposal = await proposalService.SaveProposal(proposalDto, userId);
            var user = await userRepository.GetByIdAsync(userId);
            if (user.Proposals != null)
                throw new Exception("There are no proposals for this user");
            user.Proposals?.Add(proposal);
            return await userRepository.Save();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}