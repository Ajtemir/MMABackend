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
    [Index(nameof(BuyerId), nameof(GroupDiscountProductId), IsUnique = true)]
    public class GroupDiscountProductBuyer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
        public int GroupDiscountProductId { get; set; }
        [ForeignKey(nameof(GroupDiscountProductId))]
        public GroupDiscountProduct GroupDiscountProduct { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}