using System;

namespace MMABackend.DomainModels.Common
{
    public class CollectivePurchase
    {
        
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int BuyerMinAmount { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}