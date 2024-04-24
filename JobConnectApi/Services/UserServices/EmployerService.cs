using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobConnectApi.Services;

public class EmployerService(IDataRepository<Employer> _employerRepository) : IEmployerService
{

    public async Task<Employer> GetEmployerById(string id)
    {
        Employer employer = await _employerRepository.GetByIdAsync(id);
        return employer;
    }

    public async Task<List<Employer>> GetAllEmployers()
    {
        var employers = await _employerRepository.GetAllAsync();
        var enumerableEmployers = employers.ToList();
        if (!enumerableEmployers.Any())
        {
            return new List<Employer>();
        }

        return enumerableEmployers;
        
    }

    public async Task<Employer> AddEmployer(RegisterRequest registerRequest)
    {
        var employer = new Employer
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            CompanyName = registerRequest.Company,
            Industry = registerRequest.Industry
        };

        await _employerRepository.AddAsync(employer);
        await _employerRepository.Save();
        return employer;
    }

    public async Task<bool> DeleteEmployerById(string id)
    {
        var employer = await _employerRepository.GetByIdAsync(id);
        await _employerRepository.DeleteAsync(employer);
        var isRemoved = await _employerRepository.Save();
        return isRemoved;
    }
}