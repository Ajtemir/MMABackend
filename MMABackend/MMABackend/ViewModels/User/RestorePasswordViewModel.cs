namespace MMABackend.ViewModels.User
{
    public class RestorePasswordViewModel
    {
        public string SecretWord { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}