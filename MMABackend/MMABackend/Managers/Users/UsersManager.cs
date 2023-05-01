using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using MMABackend.Configurations.Users;
using MMABackend.DomainModels.Common;

namespace MMABackend.Managers.Users
{
    public class UsersManager : IUsersManager
    {
        public string MakePasswordHashed(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var hashedBytes = md5.ComputeHash(passwordBytes);
            var hashedPassword = Encoding.ASCII.GetString(hashedBytes);
            return hashedPassword;
        }

        public string GetAccessToken(User user)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AccessTokenConfig.Issuer,
                audience: AccessTokenConfig.Audience,
                notBefore: now,
                claims: new List<Claim>
                {
                    new (AccessTokenConfig.UserIdClaim, AccessTokenConfig.GetPropertyAsIdentifier(user)),
                    new (AccessTokenConfig.UserRoleClaim, ""),
                },
                expires: now.Add(TimeSpan.FromMinutes(AccessTokenConfig.LifetimeInMinutes)),
                signingCredentials: new SigningCredentials(AccessTokenConfig.GetSymmetricSecurityKey(), AccessTokenConfig.Algorithm));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return "bearer " + encodedJwt;
        }

        public string GetRefreshToken(User user)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: RefreshTokenConfig.Issuer,
                audience: RefreshTokenConfig.Audience,
                notBefore: now,
                claims: new List<Claim>
                {
                    new (RefreshTokenConfig.UserIdClaim, RefreshTokenConfig.GetPropertyAsIdentifier(user)),
                    new (RefreshTokenConfig.UserRoleClaim, ""),
                },
                expires: now.Add(TimeSpan.FromMinutes(RefreshTokenConfig.LifetimeInMinutes)),
                signingCredentials: new SigningCredentials(RefreshTokenConfig.GetSymmetricSecurityKey(), RefreshTokenConfig.Algorithm));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return "bearer" + encodedJwt;
        }

        public async Task SendEmailAsync(string email, string secretWord)
        {
            MailAddress from = new MailAddress("aytush2001@gmail.com", "Admin");
            MailAddress to = new MailAddress(email!);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Восстановление пароля";
            m.Body = $"Кодовое слово: {secretWord}";
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("aytush2001@gmail.com", "rauojxvdweqnvted");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
    }
}