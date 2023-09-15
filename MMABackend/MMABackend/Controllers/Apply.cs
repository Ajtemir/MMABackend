using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpPost]
        public ActionResult Apply(ArgumentApply argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.BuyerEmail);
            var auctionProduct = Uow.AuctionProducts.FirstOrError(x => x.IsActive.Value && x.ProductId == argument.ProductId, 
                "Указанный товар не является аукционным");
            Uow.AuctionProductUsers.Add(new AuctionProductUser
            {
                Price = argument.SuggestedPrice,
                AuctionProductId = auctionProduct.Id,
                UserId = user.Id,
            });
            Uow.SaveChanges();
        });
    }
}