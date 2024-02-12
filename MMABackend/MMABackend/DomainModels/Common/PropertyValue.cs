using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public PropertyKey PropertyKey { get; set; }
        [JsonIgnore]

        public ICollection<ProductProperty> ProductProperties { get; set; }
    }
}