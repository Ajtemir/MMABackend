using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork
    {
        public IQueryable<AuctionProduct> ActualAuctionProductsWithOrdering => AuctionProducts
            .OrderByDescending(x => x.StartDate)
            .Where(x=>x.EndDate >= DateTime.Now && x.Status == AuctionProductStatus.Actual);
        
    }
}