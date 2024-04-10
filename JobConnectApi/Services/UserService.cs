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

    public UserService(DatabaseContext database, IJwtService jwtService, UserManager<IdentityUser> userManager)
    {
        _database = database;
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public async Task<ErrorOr<Created>> Register(RegisterRequest registerRequest)
    {
        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
        };
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user,
                registerRequest.Role); // Potential Error, Solution: Create Roles First
            return Result.Created;
        }
        
        var error = result.Errors.FirstOrDefault();
        
        return Error.Failure(code:error.Code, description:error.Description);
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
        var roles = await _userManager.GetRolesAsync(user);
        Console.WriteLine(roles.FirstOrDefault());
        
        
        if (!passwordCheckResult)
        {
            return new LoginResponse { Successful = false, Message = "Invalid password." };
        }

        // 3. Login successful (replace with token generation logic)
        var token = _jwtService.GenerateToken(user.Id, user.UserName!, "Admin");
        return new LoginResponse { Successful = true, Token = token };
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