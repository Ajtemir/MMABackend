using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using MMABackend.Enums.Common;

namespace MMABackend.DomainModels.Common
{
    public class User : IdentityUser
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }   
}