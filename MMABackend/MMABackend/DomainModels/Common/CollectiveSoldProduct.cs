using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    [Index(nameof(ProductId), nameof(IsActual), IsUnique = true)]
    public partial class CollectiveSoldProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public CollectiveProductStatus Status { get; set; } = CollectiveProductStatus.Actual;
        public bool? IsActual { get; set; } = true;
        public decimal CollectivePrice { get; set; }
        public int BuyerMinAmount { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public ICollection<CollectivePurchaser> CollectivePurchasers { get; set; } = new List<CollectivePurchaser>();
    }
}