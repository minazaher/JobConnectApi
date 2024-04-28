using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Services.UserServices;

public interface IEmployerService
{
    Task<Employer?> GetEmployerById(string id);
    Task<List<Employer?>> GetAllEmployers();
    Task<Employer?> AddEmployer(RegisterRequest request);
    Task<bool> DeleteEmployerById(string id);
}