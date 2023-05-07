using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MMABackend.DomainModels.Common
{
    public class UserAvatar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public DateTime? UploadTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public User User { get; set; }
    }
}