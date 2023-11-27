using System;
using System.Linq;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork
    {
        public IQueryable<AuctionProduct> ActualReductionProductsWithOrdering => AuctionProducts
            .OrderByDescending(x => x.StartDate)
            .Where(x=>x.EndDate >= DateTime.Now && x.Status == AuctionProductStatus.Actual && x.IsAuctionElseReduction == false);
    }
}