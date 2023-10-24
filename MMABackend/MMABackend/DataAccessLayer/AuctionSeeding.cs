using System;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class DataSeeding
    {
        private static void AuctionSeeding(this UnitOfWork uow) => uow.Execute(
            new AuctionProduct
            {
                Id = 1,
                ProductId = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = AuctionProductStatus.Actual,
                StartPrice = 25_000
            },
            new AuctionProduct
            {
                Id = 2,
                ProductId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = AuctionProductStatus.Actual,
                StartPrice = 23_000
            }
        );
    }
}