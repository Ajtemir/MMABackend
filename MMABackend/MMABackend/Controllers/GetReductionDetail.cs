using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpGet]
        public ActionResult Get([FromQuery] GetReductionDetailArgument argument) => Execute(() =>
        {
            var product = Uow.Products
                .Include(x=>x.AuctionProducts)
                .ThenInclude(x=>x.AuctionProductsUsers)
                .FirstOrError(x => x.Id == argument.ProductId);
            var isSeller = product.IsSellerById(UserId);
            ReductionState reductionState = isSeller
                ? product.ReductionProduct == null
                    ? ReductionState.SellerProductNotReductioned
                    : ReductionState.SellerProductReductioned
                : product.ReductionProduct == null 
                    ? ReductionState.BuyerProductNotReductioned
                    : product.ReductionProduct.AuctionProductsUsers.FirstOrDefault(x=>x.UserId == UserId) == null
                        ? ReductionState.BuyerNotApplied
                        : ReductionState.BuyerApplied;
            return new GetReductionDetailResult
            {
                AuctionDetail = product.ReductionProduct?.GetDetailMin,
                ReductionState = reductionState,
            };
        });
    }
}