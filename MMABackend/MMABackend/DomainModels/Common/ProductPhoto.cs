using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;

namespace MMABackend.DomainModels.Common
{
    public class ProductPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Extension => System.IO.Path.GetExtension(FileName)?.Remove(0);
        public string ContentType => Extension is null ? null : $"application/${Extension}";
        public string Path => $"/ProductImage/ProductImage/{Id}";
        [JsonIgnore]
        public DateTime? UploadTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public Product Product { get; set; }
    }
}