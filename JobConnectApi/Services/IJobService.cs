using ErrorOr;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IJobService
{
    ErrorOr<Created> CreateJob(Job job);
}