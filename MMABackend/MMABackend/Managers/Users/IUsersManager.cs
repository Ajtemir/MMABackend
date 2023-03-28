using System.Threading.Tasks;
using MMABackend.DomainModels.Common;

namespace MMABackend.Managers.Users
{
    public interface IUsersManager
    {
        string MakePasswordHashed(string password);
        string GetAccessToken(User user);
        string GetRefreshToken(User user);
        Task SendEmailAsync(string email, string secretWord = null);
    }
}