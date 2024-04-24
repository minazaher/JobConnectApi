using JobConnectApi.Database;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Services.UserServices;

public class AdminService(IJobService jobService, UserManager<Admin> userManager, DatabaseContext databaseContext)
    : IAdminService
{

    public async void SetJobAcceptedBy(string jobId, string adminId)
    {
        var admin = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        job.Admin = admin;
        job.AdminId = adminId;
        job.Status = JobStatus.Accepted;
        await databaseContext.SaveChangesAsync();
    }

    public async void SetJobRejectedBy(string jobId, string adminId)
    {
        var admin = await userManager.FindByIdAsync(adminId);
        var job = await jobService.GetJobById(jobId);
        job.Admin = admin;
        job.AdminId = adminId;
        job.Status = JobStatus.Accepted;
        await databaseContext.SaveChangesAsync();
        
    }
}