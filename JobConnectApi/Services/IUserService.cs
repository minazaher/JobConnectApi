using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Services;

public interface IUserService
{
    public ErrorOr<Created> Register(User user);
    public Task<LoginResponse> Login(LoginRequest loginRequest);
}