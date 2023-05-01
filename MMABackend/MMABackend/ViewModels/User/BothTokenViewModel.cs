namespace MMABackend.ViewModels.User
{
    public interface IAccessToken
    {
        string AccessToken { get; set; }
    } 
    
    public interface IRefreshToken
    {
        string RefreshToken { get; set; }
    }
    
    public class OnlyAccessTokenViewModel : IAccessToken
    {
        public string AccessToken { get; set; }
    }

    public class OnlyRefreshTokenViewModel : IRefreshToken
    {
        public string RefreshToken { get; set; }
    }
    
    public class BothTokenViewModel : IAccessToken, IRefreshToken
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}