using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Services.UserServices;

public interface IEmployerService
{
    Task<Employer?> GetEmployerById(string id);
    Task<List<Employer?>> GetAllEmployers();
    Task<ErrorOr<Created>> AddEmployer(RegisterRequest request);
    Task<bool> DeleteEmployerById(string id);
    // Task<List<Chat>> GetEmployerChats(string id);
    Task<bool> UpdateEmployer(string employerId, RegisterRequest registerRequest);
}