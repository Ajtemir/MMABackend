using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MMABackend.DomainModels.Common;

namespace MMABackend.Configurations.Users
{
    public static class AccessTokenConfig
    {
        public const string Issuer = "MyAuthServer"; 
        public const string Audience = "MyAuthClient";
        private const string Key = "myAccessToken secret key for authentication";  
        public const string SchemeName = "AccessToken";
        public const int LifetimeInMinutes = 60;
        public const string Algorithm = SecurityAlgorithms.HmacSha256;
        public const string UserIdClaim = ClaimTypes.Name;
        public const string UserRoleClaim = ClaimTypes.Role;
        public static readonly Func<User, string> GetPropertyAsIdentifier = user => user.Id;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new (Encoding.ASCII.GetBytes(Key));
    }
}