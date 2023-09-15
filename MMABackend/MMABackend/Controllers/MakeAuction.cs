using System;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpPost]
        public ActionResult MakeAuction(ArgumentMakeAuction argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.Email);
            var product = Uow.Products.FirstOrError(x => x.Id == argument.ProductId);
            product.ValidateSeller(user);
            Uow.AuctionProducts.ErrorIfExists(x=>x.ProductId == product.Id && x.IsActive.Value,
                "Товар уже является аукционным");
            Uow.AuctionProducts.Add(new AuctionProduct
            {
                ProductId = product.Id,
                StartPrice = argument.StartPrice,
                StartDate = argument.StartDate,
                EndDate = argument.EndDate,
                IsActive = true,
                Status = AuctionProductStatus.Actual,
            });
            Uow.SaveChanges();
        });
    }
}