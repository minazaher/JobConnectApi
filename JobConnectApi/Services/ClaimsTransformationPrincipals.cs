using System.Security.Claims;
using JobConnectApi.Services.UserServices;
using Microsoft.AspNetCore.Authentication;

namespace JobConnectApi.Services;
public class ClaimsTransformationService(IUserService userService) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return principal;
        }
        
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        string[] roles = ["Admin"];
        /*
        if (roles.Count == 0)
        {
            return principal;
        }
        */
        foreach (var role in roles)
        {
            if (principal.HasClaim(ClaimTypes.Role, role))
            {
                continue;
            }
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return principal;
    }
}