namespace MMABackend.ViewModels.User
{
    public class UserRegistrationViewModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public static implicit operator DomainModels.Common.User(UserRegistrationViewModel model)
        {
            var user = new DomainModels.Common.User
            {
                Email = model.Email,
                UserName = model.Email,
            };
            return user;
        }
    }
}