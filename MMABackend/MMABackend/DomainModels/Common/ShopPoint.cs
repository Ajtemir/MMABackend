using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    [Index(nameof(ShopId), nameof(Latitude), nameof(Longitude), IsUnique = true)]
    public class ShopPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(ShopLocationDetail))]
        public int ShopId { get; set; }
        public ShopLocationDetail ShopLocationDetail { get; set; }
        [Column(TypeName =  "Decimal(8,6)")]
        public decimal Latitude { get; set; } 
        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }
    }
}