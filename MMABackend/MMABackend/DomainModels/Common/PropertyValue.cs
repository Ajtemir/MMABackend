using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class PropertyValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PropertyKeyId { get; set; }
        [ForeignKey(nameof(PropertyKeyId))]
        public PropertyKey PropertyKey { get; set; }

        public ICollection<ProductProperty> ProductProperties { get; set; }
    }
}