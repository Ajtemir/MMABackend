using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    public class MarketShop
    {
        public int Id { get; set; }
        public int? MarketId { get; set; }
        public Market Market { get; set; }
        public int Stage { get; set; } = 1;
        public Geometry Coordinates { get; set; }
    }
}