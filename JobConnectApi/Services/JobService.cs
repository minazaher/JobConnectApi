using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Services;

public class JobService : IJobService
{
    private readonly IDataRepository<Job> _jobRepository;
    private readonly UserManager<Employer> _userManager;

    public JobService(DatabaseContext databaseContext, IDataRepository<Job> jobRepository,
        UserManager<Employer> userManager)
    {
        _jobRepository = jobRepository;
        _userManager = userManager;
    }

    public async Task<ErrorOr<Created>> CreateJob(JobRequest j, string employerId)
    {
        var employer = await _userManager.FindByIdAsync(employerId);
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
        await _jobRepository.AddAsync(job);
        bool saved = await _jobRepository.Save();
        return saved ? Result.Created : Error.Failure();
    }

    public async Task<Job> GetJobById(string id)
    {
        Job job = await _jobRepository.GetByIdAsync(id);
        return job;
    }

    public List<Job> FindAllJobs()
    {
        var jobs = _jobRepository.GetAllAsync().Result.ToList();
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

    public List<Job> FindByEmployerId(string employerId)
    {
        var jobs = FindAllJobs()
            .FindAll(j => j.EmployerId == employerId);
        return jobs;
    }
}