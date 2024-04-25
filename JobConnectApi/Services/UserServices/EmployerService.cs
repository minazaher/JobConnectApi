using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;


namespace JobConnectApi.Services.UserServices;

public class EmployerService(IDataRepository<Employer> employerRepository) : IEmployerService
{

    public async Task<Employer> GetEmployerById(string id)
    {
        Employer employer = await employerRepository.GetByIdAsync(id);
        return employer;
    }

    public async Task<List<Employer>> GetAllEmployers()
    {
        var employers = await employerRepository.GetAllAsync();
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

        await employerRepository.AddAsync(employer);
        await employerRepository.Save();
        return employer;
    }

    public async Task<bool> DeleteEmployerById(string id)
    {
        var employer = await employerRepository.GetByIdAsync(id);
        await employerRepository.DeleteAsync(employer);
        var isRemoved = await employerRepository.Save();
        return isRemoved;
    }
}