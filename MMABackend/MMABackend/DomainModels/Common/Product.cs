using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace MMABackend.DomainModels.Common
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        
        [StringLength(maximumLength: 255)] 
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; } = null;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public User User { get; set; }

        public int? ShopId { get; set; }
        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; } = new List<ProductPhoto>();
        public ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public ICollection<CollectiveSoldProduct> CollectiveSoldProducts { get; set; } =
            new List<CollectiveSoldProduct>();
        [NotMapped]
        public CollectiveSoldProduct CollectiveSoldProduct => CollectiveSoldProducts.FirstOrDefault(x => x.IsActual != null && x.IsActual.Value);
    }
}