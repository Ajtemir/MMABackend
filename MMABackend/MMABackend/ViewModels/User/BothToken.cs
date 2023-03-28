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
    
    public class OnlyAccessToken : IAccessToken
    {
        public string AccessToken { get; set; }
    }

    public class OnlyRefreshToken : IRefreshToken
    {
        public string RefreshToken { get; set; }
    }
    
    public class BothToken : IAccessToken, IRefreshToken
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}