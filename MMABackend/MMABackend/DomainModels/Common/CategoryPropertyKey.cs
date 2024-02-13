using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class CategoryPropertyKey
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(PropertyKey))]
        public int PropertyKeyId { get; set; }
        public  PropertyKey PropertyKey { get; set; }
    }
}