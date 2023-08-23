using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MMABackend.Enums.Common;
using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ShopType ShopType { get; set; } = ShopType.Online;
        public ShopLocationDetail ShopLocationDetail { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}