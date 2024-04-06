namespace JobConnectApi.Services;

public interface IJwtService
{
    public string GenerateToken(string userId, string firstName, string role);
}