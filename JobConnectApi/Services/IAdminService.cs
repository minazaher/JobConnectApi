using JobConnectApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Services;

public interface IAdminService
{
    void SetJobAcceptedBy(int jobId, string adminId);
    void SetJobRejectedBy(int jobId, string userId);
}