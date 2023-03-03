using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MMABackend.DomainModels.Common
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(maximumLength: 255)] 
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; } = null;
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public User User { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; } = new List<ProductPhoto>();
        
    }
}