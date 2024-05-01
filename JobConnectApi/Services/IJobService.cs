using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IJobService
{
    Task<Job> CreateJob(JobRequest jobRequest, string employerId);
    Task<Job> GetJobById(string id);
    List<Job> FindAllJobs();
    List<Job> GetJobsWaitingList();
    List<Job> GetActiveJobs();
    List<Job> FindByEmployerId(string employerId);
    List<Job> SearchJobsByTitle(string title);
}