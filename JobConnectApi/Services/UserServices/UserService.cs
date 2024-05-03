using JobConnectApi.Database;
using JobConnectApi.Models;
using ErrorOr;
using JobConnectApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ErrorOr.Result;

namespace JobConnectApi.Services;

public class UserService
{
    private readonly DatabaseContext _database;
    private readonly IJwtService _jwtService;
    private readonly UserManager<IdentityUser> _userManager;
    private const string RoleAdmin = "Admin";
    private const string RoleEmployer = "Employer";
    private const string RoleJobSeeker = "JobSeeker";

    public UserService(DatabaseContext database, IJwtService jwtService, UserManager<IdentityUser> userManager)
    {
        _database = database;
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public async Task<ErrorOr<Created>> Register(RegisterRequest registerRequest)
    {
        IdentityUser user;
        switch (registerRequest.Role)
        {
            case RoleAdmin:
                user = new Admin(); //TODO: To be replaced by Admin Model
                break;
            case RoleEmployer:
                user = new Employer
                {
                    CompanyName = registerRequest.Company,
                    Industry = registerRequest.Industry
                };
                break;
            case RoleJobSeeker:
                user = new JobSeeker();
                break;
            default:
                user = new IdentityUser();
                break;
        }

        user.UserName = registerRequest.UserName;
        user.Email = registerRequest.Email;

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user,
                registerRequest.Role); // Potential Error, Solution: Create Roles First
            return Result.Created;
        }

        var error = result.Errors.FirstOrDefault();

        return Error.Failure(code: error.Code, description: error.Description);
    }


    public async Task<LoginResponse> Login(LoginRequest request)
    {
        // 1. Check user existence using UserManager
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new LoginResponse { Successful = false, Message = "User not found." };
        }

        // 2. Verify password using UserManager
        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordCheckResult)
        {
            return new LoginResponse { Successful = false, Message = "Invalid password." };
        }

        var roles = await _userManager.GetRolesAsync(user);
        Console.WriteLine("User Role IS : \n");
        Console.WriteLine(roles[0]);

        // 3. Login successful (replace with token generation logic)
        var token = _jwtService.GenerateToken(user.Id, user.UserName!, roles[0]);
        return new LoginResponse { Successful = true, Token = token };
    }

    public async Task<List<Job>> GetEmployerJobs(string id)
    {
        Employer? employer = (Employer)(await _userManager.FindByIdAsync(id))!;

        var jobs = employer.PostedPosts;
        return jobs;
    }

    private bool VerifyPassword(string plainTextPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
    }

    private bool UserExists(string email) // Example usage (adapt based on your needs)
    {
        return _database.Users.Any(u => u.Email == email);
    }

    private async Task<IdentityUser?> GetUserByEmail(string email) // Example usage (adapt based on your needs)
    {
        return await _database.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    private String HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}