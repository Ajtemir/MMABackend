using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    /// <summary>
    /// Коллективные покупатели
    /// </summary>
    public class CollectivePurchaser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; }
        public int CollectiveSoldProductId { get; set; }
        public CollectiveSoldProduct CollectiveSoldProduct { get; set; }
    }
}