namespace JobConnectApi.DTOs;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; } 
    public string Role { get; set; }
    public string UserName { get; set; }
}