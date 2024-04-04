namespace JobConnectApi.DTOs;

public class LoginResponse
{
    public bool Successful { get; set; }
    public string Message { get; set; }
    public string Token { get; set; } // Placeholder for your token
}