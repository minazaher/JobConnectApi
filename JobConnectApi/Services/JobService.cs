using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Services;

public class JobService(IDataRepository<Job> jobRepository, UserManager<IdentityUser> userManager)
    : IJobService
{
    public async Task<ErrorOr<Created>> CreateJob(JobRequest j, string employerId)
    {
        var user = await userManager.FindByIdAsync(employerId);
        if (user is Employer employer)
        {
            Job job = new Job
            {
                JobId = Guid.NewGuid().ToString(),
                JobTitle = j.JobTitle,
                JobDescription = j.JobDescription,
                JobType = j.JobType,
                Salray = j.Salray,
                PostDate = j.PostDate,
                Status = JobStatus.Pending,
                IsActive = true,
                Employer = employer,
                EmployerId = employerId,
                AdminId = null,
                Admin = null
            };
            Console.WriteLine("Job is " + job);
            await jobRepository.AddAsync(job);
            bool saved = await jobRepository.Save();
            Console.WriteLine("is job saved:"+ saved );
            return saved ? Result.Created : Error.Failure();
        }
        Console.WriteLine("User Is Not Employer!"+ user.ToString());

        return Error.Unauthorized();
    }

    public async Task<Job> GetJobById(string id)
    {
        Job job = await jobRepository.GetByIdAsync(id);
        return job;
    }

    public List<Job> FindAllJobs()
    {
        var jobs = jobRepository.GetAllAsync().Result.ToList();
        return jobs;
    }

    public List<Job> GetJobsWaitingList()
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.Status == JobStatus.Pending); // Fetch all applicable jobs
        return jobs;
    }

    public List<Job> GetActiveJobs()
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.Status == JobStatus.Accepted & j.IsActive); // Fetch all jobs as a list
        return jobs;
    }

    public async Task<List<Job>> FindByEmployerId(string employerId)
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.EmployerId == employerId);
        // var employer = await userManager.FindByIdAsync(employerId);
        // if (employer is Employer emp)
        // {
        //     jobs.ForEach(j=> j.Employer = emp);
        //
        // }
        return jobs;
    }
    
}