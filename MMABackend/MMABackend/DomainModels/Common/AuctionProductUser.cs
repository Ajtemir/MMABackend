using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class AuctionProductUser
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int  AuctionProductId { get; set; }
        [ForeignKey(nameof(AuctionProductId))]
        public AuctionProduct AuctionProduct { get; set; }

        public bool IsSubmitted { get; set; } = false;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}