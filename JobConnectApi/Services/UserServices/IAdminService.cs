using ErrorOr;

namespace JobConnectApi.Services.UserServices;

public interface IAdminService
{
    Task<ErrorOr<Updated>> SetJobAcceptedBy(string jobId, string adminId);
    Task<ErrorOr<Updated>> SetJobRejectedBy(string jobId, string userId);
}