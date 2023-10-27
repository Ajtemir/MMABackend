using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpPost]
        public ActionResult UpdateAppliedAuction(ArgumentUpdateAppliedAuction argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.BuyerEmail);
            var productAuctions = Uow.AuctionProducts.OrderByDescending(x=> x.StartDate);
            var productAuction = Uow.AuctionProducts.FirstOrError(x => x.Id == argument.ProductId && x.IsActual);
            var auctionProductUser = Uow.AuctionProductUsers.FirstOrError(x => x.UserId == user.Id && x.AuctionProductId == productAuction.Id);
            auctionProductUser.Price = argument.UpdatedPrice;
            Uow.SaveChanges();
        });
    }
}