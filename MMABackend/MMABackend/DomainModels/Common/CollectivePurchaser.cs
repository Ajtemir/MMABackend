using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.DomainModels.Common
{
    /// <summary>
    /// Коллективные покупатели
    /// </summary>
    [Index(nameof(BuyerId), nameof(CollectiveSoldProductId), IsUnique = true)]
    public class CollectivePurchaser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
        public int CollectiveSoldProductId { get; set; }
        [ForeignKey(nameof(CollectiveSoldProductId))]
        public CollectiveSoldProduct CollectiveSoldProduct { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}