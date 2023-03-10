using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class IpRequestCounter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Ip { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}