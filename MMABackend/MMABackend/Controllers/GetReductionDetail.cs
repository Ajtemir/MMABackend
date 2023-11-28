using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpPost]
        public ActionResult Get([FromQuery] GetReductionDetailArgument argument) => Execute(() =>
        {
            var product = Uow.Products
                .Include(x=>x.AuctionProducts)
                .ThenInclude(x=>x.AuctionProductsUsers)
                .FirstOrError(x => x.Id == argument.ProductId);
            var isSeller = product.IsSellerById(UserId);
            ReductionState reductionState = isSeller
                ? product.AuctionProduct == null
                    ? ReductionState.SellerProductNotAuctioned
                    : ReductionState.SellerProductAuctioned
                : product.AuctionProduct == null 
                    ? ReductionState.BuyerProductNotAuctioned
                    : product.AuctionProduct.AuctionProductsUsers.FirstOrDefault(x=>x.UserId == UserId) == null
                        ? ReductionState.BuyerNotApplied
                        : ReductionState.BuyerApplied;
            return new GetReductionDetailResult
            {
                AuctionDetail = product.AuctionProduct?.GetDetailMin,
                ReductionState = reductionState,
            };
        });
    }
}