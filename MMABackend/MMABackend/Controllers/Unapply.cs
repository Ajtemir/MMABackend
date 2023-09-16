using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        public ActionResult Unapply(ArgumentUnapply argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.BuyerEmail);
            var auctionProduct = Uow.AuctionProductUsers.FirstOrError(x =>  
                x.ProductId == argument.ProductId &&
                x.UserId == argument.BuyerEmail, 
                "Указанный товар не является аукционным");
            Uow.Remove(auctionProduct);
            Uow.SaveChanges();
        });
    }
}