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
    public async Task<Job> CreateJob(JobRequest j, string employerId)
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
                Industry = j.Industry,
                Location = j.Location,
                EmployerId = employerId,
                AdminId = null,
                Admin = null
            };
            Console.WriteLine("Job is " + job);
            await jobRepository.AddAsync(job);
            bool saved = await jobRepository.Save();
            Console.WriteLine("is job saved:" + saved);
            return saved ? job : throw new Exception("Cannot save Job");
        }


        throw new UnauthorizedAccessException("You are not authorized to create jobs");
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
            .FindAll(j => j is
            {
                IsActive: true, Status: JobStatus.Accepted
            }); 
        return jobs;
    }

    public List<Job> FindByEmployerId(string employerId)
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.EmployerId == employerId).ToList();

        return jobs.IsNullOrEmpty() ? throw new KeyNotFoundException() : jobs;
    }

    public List<Job> SearchJobsByTitle(string title)
    {
        List<Job> jobs = GetActiveJobs().FindAll(j => j.JobTitle.Normalize().Contains(title.Normalize())).ToList();
        return jobs.Count == 0 ? new List<Job>() : jobs;
    }

    public async Task<bool> DeActivateJob(string jobId)
    {
        Job job = await GetJobById(jobId);
        job.IsActive = false;
        await jobRepository.UpdateAsync(job);
        return await jobRepository.Save();
    }
}