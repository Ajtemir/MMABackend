﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    [Index(nameof(IsActive), nameof(ProductId), IsUnique = true)]
    public class AuctionProduct
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public bool? IsActive { get; set; } = null;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } 
        public decimal StartPrice { get; set; }
        public AuctionProductStatus Status { get; set; } = AuctionProductStatus.Actual;
        public ICollection<AuctionProductUser> AuctionProductsUsers { get; set; } = new List<AuctionProductUser>();
        [NotMapped]
        public AuctionProductUser AuctionProductUser => AuctionProductsUsers.FirstOrDefault();

        public void Deactivate()
        {
            Status = AuctionProductStatus.Canceled;
            IsActive = null;
        }
    }
}