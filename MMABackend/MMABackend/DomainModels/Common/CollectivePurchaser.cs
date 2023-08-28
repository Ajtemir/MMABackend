using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
        public int CollectiveSoldProductId { get; set; }

        public ICollection<CollectiveSoldProduct> CollectiveSoldProducts { get; set; } =
            new List<CollectiveSoldProduct>();
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}