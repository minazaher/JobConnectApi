using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public class JobService: IJobService
{
    private DatabaseContext _databaseContext;

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
}