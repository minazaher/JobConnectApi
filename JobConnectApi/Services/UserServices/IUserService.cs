using ErrorOr;
using JobConnectApi.DTOs;

namespace JobConnectApi.Services.UserServices;

public interface IUserService
{
    public Task<ErrorOr<Created>> Register(RegisterRequest user);
    
  //  public Task<LoginResponse> Login(LoginRequest loginRequest);
}