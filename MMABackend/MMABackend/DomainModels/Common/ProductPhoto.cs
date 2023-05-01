using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;

namespace MMABackend.DomainModels.Common
{
    public class ProductPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public DateTime? UploadTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public Product Product { get; set; }
    }
}