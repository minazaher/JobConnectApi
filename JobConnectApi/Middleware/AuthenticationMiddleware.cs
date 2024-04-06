using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Middleware;

public class AuthenticationMiddleware(RequestDelegate next, string secretKey)
{
    public async Task<AuthenticateResult> Invoke(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"].ToString();
        Console.WriteLine("We reached this point with header " + authHeader);
        if (string.IsNullOrEmpty(authHeader))
        {
            context.Items["isAuth"] = false; // Assuming 'isAuth' for authentication state
            await next(context);
            return AuthenticateResult.Fail("Failed");
        }

        var token = authHeader.Split(' ').Skip(1).FirstOrDefault(); // Assuming 'Bearer' scheme
        Console.WriteLine("We reached this point with token " + token);
        if (string.IsNullOrEmpty(token))
        {
            context.Items["isAuth"] = false;
            await next(context);
            return AuthenticateResult.Fail("Failed");
        }
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false, // Or set issuer validation if needed
            ValidateAudience = false, // Or set audience validation if needed
            ValidateLifetime = true // Validate token expiration
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal =
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
        var userId = principal.Claims.FirstOrDefault(c => c.Type == "userId")?.Value; // Assuming 'userId' claim
        context.Items["isAuth"] = true;
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };
        var identity = new ClaimsIdentity(claims, "Basic");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        Console.WriteLine("Authentication Res : "
                          + AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "Scheme")));
        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "S"));
    }
}
