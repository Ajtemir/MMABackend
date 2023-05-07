using Microsoft.AspNetCore.Http;

namespace MMABackend.ViewModels.User
{
    public class SetProfilePhotoViewModel
    {
        public IFormFile Avatar { get; set; }
    }
}