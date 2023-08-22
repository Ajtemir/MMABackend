using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName =  "Decimal(8,6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }
        public ICollection<Shop> Shops { get; set; } = new List<Shop>();
    }
}