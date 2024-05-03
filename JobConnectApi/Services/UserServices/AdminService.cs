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
    public async Task<bool> SetJobAcceptedBy(string jobId, string adminId) // TODO use errorOr
    {
        var user = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        if (user is Admin admin)
        {
            job.Admin = admin;
            job.AdminId = adminId;
            job.Status = JobStatus.Accepted;
            Console.WriteLine($"Job status is {job.Status}");
            await dataRepository.UpdateAsync(job);
            return await dataRepository.Save();
        }
        else
        {
            Console.WriteLine("User is not admin");
        }
        return false;
    }

    public async Task<bool> SetJobRejectedBy(string jobId, string adminId)
    {
        var user = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        if (user is Admin admin)
        {
            job.Admin = admin;
            job.AdminId = adminId;
            job.Status = JobStatus.Rejected;
        }
        Console.WriteLine($"Job is {job}");
        await dataRepository.UpdateAsync(job);
        return await dataRepository.Save();
    }
}