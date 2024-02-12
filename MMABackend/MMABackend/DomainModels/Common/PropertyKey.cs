using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MMABackend.DomainModels.Common
{
    public class PropertyKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsMultipleOrLiteralDefault { get; set; } = false;
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore]
        public Category Category { get; set; }
        [JsonIgnore]
        public ICollection<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
        
        
        public override bool Equals(object obj)
        {
            var item = obj as PropertyKey;

            if (item == null)
            {
                return false;
            }

            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
