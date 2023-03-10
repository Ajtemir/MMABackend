using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    public class Shop
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Point Coordinate { get; set; } = null;
        public bool IsSealed { get; set; } = false;
        public int? MarketShopId { get; set; }
        public MarketShop MarketShop { get; set; }
    }
}