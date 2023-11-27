using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Make([FromBody]MakeReductionArgument argument) => Execute(() =>
        {
            var product = Uow.Products.FirstOrError(x => x.Id == argument.ProductId);
            product.ValidateSellerById(UserId);
            Uow.ActualReductionProductsWithOrdering.ErrorIfExists(x=>x.ProductId == product.Id,
                "Товар уже является тендерным");
            Uow.AuctionProducts.Add(new AuctionProduct
            {
                ProductId = product.Id,
                StartPrice = argument.StartPrice,
                StartDate = argument.StartDate,
                EndDate = argument.EndDate,
                Status = AuctionProductStatus.Actual,
                IsAuctionElseReduction = false,
            });
            Uow.SaveChanges();
        });
    }
}