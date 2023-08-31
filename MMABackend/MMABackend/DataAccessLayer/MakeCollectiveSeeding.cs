using System;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void MakeCollectiveSeeding(this UnitOfWork uow) => uow.Execute(
            new CollectiveSoldProduct
            {
                Id = 1,
                ProductId = 2,
                IsActual = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = CollectiveProductStatus.Actual,
                CollectivePrice = 30M,
                BuyerMinAmount = 5,
            },
            new CollectiveSoldProduct
            {
                Id = 2,
                ProductId = 1,
                IsActual = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = CollectiveProductStatus.Actual,
                CollectivePrice = 25M,
                BuyerMinAmount = 2,
            }
        );
    }
}