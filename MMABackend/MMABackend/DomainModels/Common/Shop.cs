using System.ComponentModel.DataAnnotations.Schema;
using MMABackend.Enums.Common;
using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    public class Shop
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ShopType ShopType { get; set; } = ShopType.Online;
        [Column(TypeName =  "Decimal(8,6)")]
        public decimal? Latitude { get; set; } = null;
        [Column(TypeName = "Decimal(9,6)")]
        public decimal? Longitude { get; set; } = null;
        public MarketShop MarketShop { get; set; }
    }
}