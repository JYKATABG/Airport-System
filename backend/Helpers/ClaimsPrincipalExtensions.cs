using System;
using System.Security.Claims;


namespace backend.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var rawId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(rawId, out var userId))
        {
            throw new UnauthorizedAccessException("Token does not contain a valid numeric user identifier.");
        }
        return userId;
    }
}
