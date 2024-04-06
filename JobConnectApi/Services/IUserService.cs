using ErrorOr;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Services;

public interface IUserService
{
    public Task<ErrorOr<Created>> Register(RegisterRequest user);
  //  public Task<LoginResponse> Login(LoginRequest loginRequest);
}