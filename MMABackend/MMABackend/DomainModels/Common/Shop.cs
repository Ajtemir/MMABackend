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
        public ShopLocationDetail ShopLocationDetail { get; set; }
    }
}