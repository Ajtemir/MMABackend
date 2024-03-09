using System;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void MakeCollectiveSeeding(this UnitOfWork uow) => uow.Execute(
            new GroupDiscountProduct
            {
                Id = 1,
                ProductId = 2,
                IsActual = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = CollectiveProductStatus.Actual,
                GroupDiscountPrice = 30M,
                BuyerMinAmount = 5,
            },
            new GroupDiscountProduct
            {
                Id = 2,
                ProductId = 1,
                IsActual = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = CollectiveProductStatus.Actual,
                GroupDiscountPrice = 25M,
                BuyerMinAmount = 2,
            }
        );
    }
}