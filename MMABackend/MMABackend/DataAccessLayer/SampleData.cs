using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public static class SampleData
    {
        public const string EmailPostfix = "@example.com";
        public static readonly string[] Logins = new[] { "zero", "first", "second", "third", "fourth", "fifth" };
        public static string GetEmailByIndex(int index) => Logins[index] + EmailPostfix;
        public static void InitializeUsersAndRoles(this IServiceProvider serviceProvider)
        {
            UnitOfWork context = serviceProvider?.GetService<UnitOfWork>() ?? throw new ArgumentNullException(nameof(serviceProvider));

            string[] roles =  { "User", "Administrator" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                if (!context.Roles.Any(r => r.Name == role)) roleStore.CreateAsync(new IdentityRole(role));
                
            }

            
            var number = "+111111111111";
            foreach (var login in Logins)
            {
                var user = new User
                {
                    Email = login + EmailPostfix,
                    NormalizedEmail = login.ToUpper() + "@EXAMPLE.COM",
                    UserName = login,
                    NormalizedUserName = login.ToUpper(),
                    PhoneNumber = number,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };


                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user,"password");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<User>(context);
                    var result = userStore.CreateAsync(user);
                }
                AssignRoles(serviceProvider, user.Email, roles);
            }
            context.SaveChangesAsync();
        }

        private static async Task AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<User> userManager = services.GetService<UserManager<User>>()!;
            User user = await userManager.FindByEmailAsync(email);
            await userManager.AddToRolesAsync(user, roles);
        }

    }
}