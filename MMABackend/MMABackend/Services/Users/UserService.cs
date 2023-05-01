using System;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using MMABackend.Configurations.Users;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Managers.Users;
using MMABackend.ViewModels.User;

namespace MMABackend.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _uow;
        private readonly IUsersManager _manager;
        private readonly IMemoryCache _cache;

        public UserService(UnitOfWork uow, IUsersManager manager, IMemoryCache cache)
        {
            _uow = uow;
            _manager = manager;
            _cache = cache;
        }

        public void AddUser(User user)
        {
            if (_uow.Users.FirstOrDefault(x => x.Email == user.Email) is not null) 
                throw new Exception("User already has registered");
            // user.Password = _manager.MakePasswordHashed(user.Password);
            _uow.Users.Add(user);
            _uow.SaveChanges();
        }

        public User GetUserByCredentials(UserCredentialsViewModel credentials)
        {
            var user = _uow.Users.FirstOrDefault(x => x.Email == credentials.Email);
            if (user is null) throw new Exception("User by email not found");
            var hashedPassword = _manager.MakePasswordHashed(credentials.Password);
            // if (user.Password != hashedPassword) throw new Exception("Incorrect user's password");
            return user;
        }

        public BothTokenViewModel GetBothTokens(User user)
        {
            var accessToken = _manager.GetAccessToken(user);
            var refreshToken = _manager.GetRefreshToken(user);
            return new BothTokenViewModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void SetSecretWordForRestorePasswordInCache(string email, string secretWord)
        {
            _cache.Set(SecretWordCacheStoringConfig.JoinPrefixAndEmailForReset(email), secretWord, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SecretWordCacheStoringConfig.LifeTimeInMinutes),
            });
        }

        public void SetSecretWordForConfirmEmailInCache(ConfirmPasswordViewModel model)
        {
            _cache.Set(
                SecretWordCacheStoringConfig.JoinPrefixAndEmailForConfirmEmail(model.Email), model.SecretWord, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SecretWordCacheStoringConfig.LifeTimeInMinutes),
                });
        }

        public void RestorePassword(RestorePasswordViewModel model)
        {
            if (_cache.TryGetValue(SecretWordCacheStoringConfig.JoinPrefixAndEmailForReset(model.Email), out string secretWord))
            {
                if (secretWord == model.SecretWord)
                {
                    var user = _uow.Users.FirstOrDefault(x => x.Email == model.Email) ??
                               throw new Exception("As restoring password user entity not found");
                    // user.Password = _manager.MakePasswordHashed(model.NewPassword);
                    _uow.SaveChanges();
                }
                throw new Exception("Secret words are not same." +
                                    "Maybe you should retry again.");
            }
            throw new Exception("Secret word not found from cache." +
                                " Maybe store has expired." +
                                " Try again, request another secret word");
        }
    }
}