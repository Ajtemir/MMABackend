using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    [Index(nameof(IsActive), nameof(ProductId), IsUnique = true)]
    public class AuctionProduct
    {
        [Key]
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public bool? IsActive { get; set; } = null;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = null;
        public ICollection<AuctionUser> AuctionUsers { get; set; } = new List<AuctionUser>();
    }
}