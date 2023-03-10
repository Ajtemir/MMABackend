using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MMABackend.Enums.Common;

namespace MMABackend.DomainModels.Common
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }   
}