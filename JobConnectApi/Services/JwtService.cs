using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Services;
using System.IdentityModel.Tokens.Jwt;

using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

public class JwtService: IJwtService
{
    public string GenerateToken(Guid userId, string firstName, string role)
    {
        var signingCredentials =
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-super-secret-key")),
                SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.GivenName, firstName),
        };
        var token = new JwtSecurityToken(
            issuer: "jobConnect",
            expires: DateTime.Now.AddDays(1),
            claims: claims,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}