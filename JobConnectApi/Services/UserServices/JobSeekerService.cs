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

    public async Task<ErrorOr<Created>> SubmitProposal(string userId, SubmitProposalDto proposalDto)
    {
        Proposal proposal = await proposalService.SaveProposal(proposalDto, userId);
        var user = await userRepository.GetByIdAsync(userId);
        if (user.Proposals != null)
            user.Proposals.Add(proposal);
        else
            return Error.Failure("Proposals is null");
        return await userRepository.Save() ? Result.Created : Error.Failure();
    }
}