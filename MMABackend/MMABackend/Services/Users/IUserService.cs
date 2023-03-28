using MMABackend.DomainModels.Common;
using MMABackend.ViewModels.User;

namespace MMABackend.Services.Users
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserByCredentials(UserCredentials credentials);
        BothToken GetBothTokens(User user);
        void RestorePassword(RestorePasswordViewModel model);
        void SetSecretWordForRestorePasswordInCache(string email, string secretWord);
        void SetSecretWordForConfirmEmailInCache(ConfirmPasswordViewModel model);

    }
}