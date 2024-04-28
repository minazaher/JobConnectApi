using ErrorOr;

namespace JobConnectApi.Services.UserServices;

public interface IAdminService
{
    Task<bool> SetJobAcceptedBy(string jobId, string adminId);
    Task<bool> SetJobRejectedBy(string jobId, string userId);
}