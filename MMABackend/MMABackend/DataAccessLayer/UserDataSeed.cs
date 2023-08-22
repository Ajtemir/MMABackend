using System;
using System.Collections;
using System.Collections.Generic;
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
        public static readonly string[] Logins = new[] { 
            UserIds.zero.ToString(),
            UserIds.first.ToString(),
            UserIds.second.ToString(),
            UserIds.third.ToString(),
            UserIds.fourth.ToString(),
            UserIds.fifth.ToString(),
            
        };
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
            FillAvatars(context);
        }

        private static async Task AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<User> userManager = services.GetService<UserManager<User>>()!;
            User user = await userManager.FindByEmailAsync(email);
            await userManager.AddToRolesAsync(user, roles);
        }

        private static async void FillAvatars(UnitOfWork uow)
        {
            var avatars = GetAvatars(uow);
            uow.UserAvatars.AddRange(avatars);
            await uow.SaveChangesAsync();
        }

        private static IEnumerable<UserAvatar> GetAvatars(UnitOfWork uow)
        {
            return from login in Logins let path = "/avatars/" let ext = ".jpg" select new UserAvatar
            {
                Path = path + login + ext,
                UserId = uow.Users.FirstOrDefault(x => x.UserName.Equals(login))?.Id,
            };
        }

    }

    public enum UserIds
    {
        zero = 0,
        first = 1,
        second = 2,
        third = 3,
        fourth = 4,
        fifth = 5,
    }
}