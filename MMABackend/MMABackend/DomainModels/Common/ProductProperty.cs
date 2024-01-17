using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    [Index(nameof(ProductId), nameof(PropertyValueId), IsUnique = true)]
    public class ProductProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(PropertyKey))]
        public int PropertyKeyId { get; set; }
        public PropertyKey PropertyKey { get; set; }
        [ForeignKey(nameof(PropertyValue))]
        public int? PropertyValueId { get; set; }
        public PropertyValue PropertyValue { get; set; }
        public int? NumberValue { get; set; }
      
    }
    
    
}