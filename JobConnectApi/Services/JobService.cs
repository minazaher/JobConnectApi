using System.Text.Json;
using System.Text.Json.Serialization;
using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Services;

public class JobService(
    IDataRepository<Job> jobRepository,
    DatabaseContext databaseContext,
    UserManager<IdentityUser> userManager,
    IDataRepository<Employer> employerRepository)
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
            Console.WriteLine("is job saved:" + saved);
            return saved ? Result.Created : Error.Failure();
        }

        Console.WriteLine("User Is Not Employer!" + user.ToString());

        return Error.Unauthorized();
    }

    public async Task<Job> GetJobById(string id)
    {
        var job = await jobRepository.GetByIdAsync(id);

        var employer = await employerRepository.GetByIdAsync(job.EmployerId!);
        if (job.Employer != null) job.Employer.UserName = employer.UserName;
        return job ?? throw new KeyNotFoundException("Job Not found");
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
            .FindAll(j => j.IsActive); // Fetch all jobs as a list //TODO check if accepted
        return jobs;
    }

    public ErrorOr<List<Job>> FindByEmployerId(string employerId)
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.EmployerId == employerId);

        return jobs.IsNullOrEmpty() ? Error.NotFound() : jobs;
    }

    public List<Job> SearchJobsByTitle(string title)
    {
        List<Job> jobs = FindAllJobs().FindAll(j => j.JobTitle.Normalize().Contains(title.Normalize())).ToList();
        return jobs.Count == 0 ? new List<Job>() : jobs;
    }
}