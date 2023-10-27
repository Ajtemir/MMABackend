using System.Linq;
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
            var auctionProduct = Uow.ActualAuctionProductsWithOrdering.FirstOrError(x=>x.ProductId == argument.ProductId, 
                "Товар не является аукционным чтобы убрать из акции");
            var user = Uow.GetUserByEmailOrError(argument.BuyerEmail);
            var auctionProductUser = Uow.AuctionProductUsers.FirstOrError(x =>  
                x.AuctionProductId == auctionProduct.Id &&
                x.UserId == user.Id,
                "Вы не подавались к акционной покупке");
            Uow.Remove(auctionProductUser);
            Uow.SaveChanges();
        });
    }
}