namespace JobConnectApi.Services.UserServices;

public interface IAdminService
{
    void SetJobAcceptedBy(string jobId, string adminId);
    void SetJobRejectedBy(string jobId, string userId);
}