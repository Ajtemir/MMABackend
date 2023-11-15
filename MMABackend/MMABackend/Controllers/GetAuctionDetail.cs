using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpGet]
        public ActionResult<GetAuctionDetailResult> GetAuctionDetail([FromQuery]ArgumentGetAuctionDetail argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.Email);
            var product = Uow.Products
                .Include(x=>x.AuctionProducts)
                .FirstOrError(x => x.Id == argument.ProductId);
            var isSeller = product.IsSeller(user);
            AuctionState auctionState = isSeller
                ? product.AuctionProduct == null
                    ? AuctionState.SellerProductNotAuctioned
                    : AuctionState.SellerProductAuctioned
                : product.AuctionProduct == null 
                    ? AuctionState.BuyerProductNotAuctioned
                    : product.AuctionProduct.AuctionProductsUsers.FirstOrDefault(x=>x.UserId == user.Id) == null
                        ? AuctionState.BuyerNotApplied
                        : AuctionState.BuyerApplied;
            return new GetAuctionDetailResult
            {
                AuctionDetail = product.AuctionProduct?.GetDetail,
                AuctionState = auctionState,

            };
        });
    }

    public class ArgumentGetAuctionDetail
    {
        public int ProductId { get; set; }
        public string Email { get; set; }
    }

    public class GetAuctionDetailResult
    {
        public AuctionDetail AuctionDetail { get; set; }
        public AuctionState AuctionState { get; set; }
    }
}