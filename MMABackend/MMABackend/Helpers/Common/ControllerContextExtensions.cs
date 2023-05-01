using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MMABackend.Configurations.Users;

namespace MMABackend.Helpers.Common
{
    public static class ControllerContextExtensions
    {
        public static string GetEmailFromContext(this HttpContext context)
        {
            var idClaims = context.User
                .Claims.FirstOrDefault(x => x.Type == AccessTokenConfig.UserIdClaim);
                var email = idClaims?.Value ?? 
                throw new Exception("While getting access token by refresh token user id claim not found");
            return email;
        }
    }
}