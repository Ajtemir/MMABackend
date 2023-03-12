using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    public class Shop
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName =  "Decimal(8,6)")]
        public decimal? Latitude { get; set; } = null;
        [Column(TypeName = "Decimal(9,6)")]
        public decimal? Longitude { get; set; } = null;
        public bool IsSealed { get; set; } = false;
        public int? MarketShopId { get; set; } = null;
        public MarketShop MarketShop { get; set; }
    }
}