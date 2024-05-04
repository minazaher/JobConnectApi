using ErrorOr;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;


namespace JobConnectApi.Services.UserServices;

public class EmployerService(IDataRepository<Employer?> employerRepository, UserManager<IdentityUser> userManager, UserService userService )
    : IEmployerService
{
    public async Task<Employer?> GetEmployerById(string id)
    {
        Employer? employer = await employerRepository.GetByIdAsync(id);
        return employer;
    }

    public async Task<List<Employer?>> GetAllEmployers()
    {
        var employers = await employerRepository.GetAllAsync();
        var enumerableEmployers = employers.ToList();
        if (!enumerableEmployers.Any())
        {
            return new List<Employer?>();
        }

        return enumerableEmployers;
    }

    public async Task<ErrorOr<Created>>AddEmployer(RegisterRequest registerRequest)
    {
        return await userService.Register(registerRequest);
        /*
        var existingEmp = await userManager.FindByEmailAsync(registerRequest.Email);
        if (existingEmp == null)
        {
            throw new Exception("User Already Exists");
        }

        var employer = new Employer
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            CompanyName = registerRequest.Company,
            Industry = registerRequest.Industry
        };

        await employerRepository.AddAsync(employer);

        var saved = await employerRepository.Save();

        if (saved)
        {
            return employer;
        }

        throw new Exception("Internal Server Error, Employer Cannot Be Saved")*/
    }

    public async Task<bool> DeleteEmployerById(string id)
    {
        var employer = await employerRepository.GetByIdAsync(id);
        if (employer == null)
        {
            return false;
        }

        await employerRepository.DeleteAsync(employer);
        var isRemoved = await employerRepository.Save();
        return isRemoved;
    }
}