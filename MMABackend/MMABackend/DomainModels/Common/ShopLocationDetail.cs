using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace MMABackend.DomainModels.Common
{
    [Table("ShopLocationDetails")]
    public class ShopLocationDetail : Shop
    {
        public int? MarketId { get; set; }
        public Market Market { get; set; }
        public int Stage { get; set; } = 1;
        [Column(TypeName =  "Decimal(8,6)")]
        public decimal? Latitude { get; set; } = null;
        [Column(TypeName = "Decimal(9,6)")]
        public decimal? Longitude { get; set; } = null;
    }
}