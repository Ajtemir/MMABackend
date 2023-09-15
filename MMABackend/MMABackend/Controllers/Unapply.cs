using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpDelete]
        public ActionResult Unapply(ArgumentUnapply argument) => Execute(() =>
        {
            var auctionProduct = Uow.AuctionProducts.FirstOrError(x=>x.ProductId == argument.ProductId && x.IsActive.Value);
            var user = Uow.GetUserByEmailOrError(argument.BuyerEmail);
            var auctionProductUser = Uow.AuctionProductUsers.FirstOrError(x =>  
                x.AuctionProductId == auctionProduct.Id &&
                x.UserId == user.Id,
                "Указанный товар не является аукционным");
            Uow.Remove(auctionProductUser);
            Uow.SaveChanges();
        });
    }
}