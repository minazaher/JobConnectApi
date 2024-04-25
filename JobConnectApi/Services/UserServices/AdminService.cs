using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Services.UserServices;

public class AdminService(
    IJobService jobService,
    IDataRepository<Job> dataRepository,
    UserManager<IdentityUser> userManager,
    DatabaseContext databaseContext)
    : IAdminService
{
    public async Task<ErrorOr<Updated>> SetJobAcceptedBy(string jobId, string adminId) // TODO use errorOr
    {
        var user = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        if (user is Admin admin)
        {
            job.Admin = admin;
            job.AdminId = adminId;
            job.Status = JobStatus.Accepted;
        }

        await dataRepository.UpdateAsync(job);
        return await dataRepository.Save() ? Result.Updated : Error.Failure(description: "Something went wrong");

    }

    public async Task<ErrorOr<Updated>> SetJobRejectedBy(string jobId, string adminId)
    {
        var user = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        if (user is Admin admin)
        {
            job.Admin = admin;
            job.AdminId = adminId;
            job.Status = JobStatus.Accepted;
        }
        await dataRepository.UpdateAsync(job);
        return await dataRepository.Save() ? Result.Updated : Error.Failure(description: "Something went wrong");    }
}