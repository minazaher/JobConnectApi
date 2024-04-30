using AutoMapper;
using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Services.UserServices;

public class JobSeekerService(
    IDataRepository<Job> jobRepository,
    IMapper mapper,
    IDataRepository<JobSeeker> userRepository,
    DatabaseContext databaseContext,
    IProposalService proposalService) : IJobSeekerService
{
    public async Task<ErrorOr<Updated>> AddToSavedJobs(string userId, string jobId)
    {
        try
        {
            // 1. Fetch Job and JobSeeker (eager loading for SavedJobs)
            var jobSeeker = await databaseContext.JobSeekers
                .Include(j => j.SavedJobs)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var job = await databaseContext.Jobs
                .Include(j => j.SavedBy)
                .Where(u => u.JobId == jobId)
                .FirstOrDefaultAsync();

            // 2. Check existence and avoid null references
            if (jobSeeker == null || job == null)
            {
                return Error.NotFound(description: "Job or JobSeeker not found");
            }

            // 3. Add job to SavedJobs collection (if not already present)
            if (!jobSeeker.SavedJobs.Any(j => j.JobId == job.JobId)) // Check for existing job
            {
                jobSeeker.SavedJobs.Add(job);
            }

            // 4. Save changes using DbContext
            await databaseContext.SaveChangesAsync();

            return Result.Updated;
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine("Job or JobSeeker not found");
            Console.WriteLine(e);
            return Error.NotFound(description: "Job or JobSeeker not found");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving job to saved jobs");
            Console.WriteLine(e);
            return Error.Failure(description: "Error saving job to saved jobs");
        }
    }


    public async Task<List<JobResponse>?> GetUserSavedJobs(string userId)
    {
        
        // Eager loading for SavedJobs (including Jobs)
        var jobSeeker = await databaseContext.JobSeekers
            .Include(j => j.SavedJobs)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        var jobResponses = jobSeeker?.SavedJobs?.Select(mapper.Map<JobResponse>).ToList();

        // Return the saved jobs (if any)
        return jobResponses.IsNullOrEmpty()? new List<JobResponse>(): jobResponses;
    }

    public async Task<ErrorOr<Updated>> RemoveFromSavedJobs(string userId, string jobId)
    {
        try
        {
            // 1. Fetch Job and JobSeeker (eager loading for SavedJobs)
            var jobSeeker = await databaseContext.JobSeekers
                .Include(j => j.SavedJobs)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var job = await databaseContext.Jobs
                .Include(j => j.SavedBy)
                .Where(u => u.JobId == jobId)
                .FirstOrDefaultAsync();

            // 2. Check existence and avoid null references
            if (jobSeeker == null)
            {
                return Error.NotFound(description: "JobSeeker not found");
            }
            if (job == null)
            {
                return Error.NotFound(description: "Job not found");
            }

            // 3. Add job to SavedJobs collection (if not already present)
            if (jobSeeker.SavedJobs.Any(j => j.JobId == job.JobId)) // Check for existing job
            {
                jobSeeker.SavedJobs.Remove(job);
                Console.WriteLine("Saved Jobs After Removal are " + jobSeeker.SavedJobs);
            }

            // 4. Save changes using DbContext
            await databaseContext.SaveChangesAsync();

            return Result.Updated;
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine("Job or JobSeeker not found");
            Console.WriteLine(e);
            return Error.NotFound(description: "Job or JobSeeker not found");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving job to saved jobs");
            Console.WriteLine(e);
            return Error.Failure(description: "Error saving job to saved jobs");
        }
        
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