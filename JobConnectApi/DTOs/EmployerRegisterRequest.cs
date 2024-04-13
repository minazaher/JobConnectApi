namespace JobConnectApi.DTOs;

public struct EmployerRegisterRequest
{
    private RegisterRequest DefaultRequest { get; set; }
    private string Company { get; set; }
    private string Industry { get; set; }
}