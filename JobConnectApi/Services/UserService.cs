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
        var hashedPassword = HashPassword(registerRequest.Password);
        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
        };
        var result = await _userManager.CreateAsync(user, hashedPassword);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user,
                registerRequest.Role); // Potential Error, Solution: Create Roles First
            return Result.Created;
        }

        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Description);
        }

        return Error.Failure();
    }

/*
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        // 1. Check user existence
        if (!UserExists(request.Email)) // Call user existence check method
        {
            return new LoginResponse { Successful = false, Message = "User not found." };
        }

        // 2. Retrieve the user from the database
        var user = await GetUserByEmail(request.Email); // Call user retrieval method

        // 3. Verify password (replace with your actual implementation)
        if (!VerifyPassword(request.Password, user.Password)) // Implement secure password verification
        {
            return new LoginResponse { Successful = false, Message = "Invalid password." };
        }

        // 4. Login successful (replace with token generation logic)
        var token = jwtService.GenerateToken(user.UserId, user.FirstName, user.LastName);
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
    */
    private String HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}