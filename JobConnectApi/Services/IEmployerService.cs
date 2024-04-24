using Google.Protobuf.WellKnownTypes;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using MaybeSharp;

namespace JobConnectApi.Services;

public interface IEmployerService
{
    Task<Employer> GetEmployerById(string id);
    Task<List<Employer>> GetAllEmployers();
    Task<Employer> AddEmployer(RegisterRequest request);
    Task<bool> DeleteEmployerById(string id);
}