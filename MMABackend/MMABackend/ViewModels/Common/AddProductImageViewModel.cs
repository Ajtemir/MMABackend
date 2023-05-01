using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MMABackend.ViewModels.Common
{
    public class AddProductImageViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}