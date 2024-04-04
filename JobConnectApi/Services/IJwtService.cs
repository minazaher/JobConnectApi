namespace JobConnectApi.Services;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string firstName, string lastName);
}