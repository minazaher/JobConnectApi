using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public class JobService: IJobService
{
    private readonly DatabaseContext _databaseContext;

    public JobService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public ErrorOr<Created> CreateJob(Job job)
    {
        _databaseContext.Add(job);
        _databaseContext.SaveChanges();
        return Result.Created;
    }
    
    public async Task<Job> GetJobById(int id)
    {
        Job job = (await _databaseContext.Jobs.FindAsync(id))!;
        return job;
    }

    public List<Job> FindAll()
    {
        var jobs = _databaseContext.Jobs.ToList();  // Fetch all jobs as a list
        return jobs;
    }
}