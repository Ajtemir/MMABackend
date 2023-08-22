using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public class CollectiveSoldProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool? IsActual { get; set; }
        public decimal CollectivePrice { get; set; }
        public int BuyerMinAmount { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}