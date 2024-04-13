using ErrorOr;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IJobService
{
    ErrorOr<Created> CreateJob(Job job);
    Task<Job> GetJobById(int id);
    List<Job> FindAll();
    List<Job> GetJobsWaitingList();
}