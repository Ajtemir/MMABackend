using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        [HttpPost]
        public ActionResult SubmitAuction(ArgumentSubmitAuction argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.Email);
            var product = Uow.Products.FirstOrError(x => x.Id == argument.ProductId);
            product.ValidateSeller(user);
            var auctionProduct = Uow.ActualAuctionProductsWithOrdering
                .Include(x=>x.AuctionProductsUsers)
                .FirstOrError(x=>x.ProductId == product.Id,
                "Аукционный товар не найден");
            var auctionProductUser = auctionProduct.AuctionProductsUsers.OrderByDescending(x => x.Price)
                .FirstOrError(errorMessage : "У товара нет желающих покупателей");
            auctionProduct.Status = AuctionProductStatus.Submitted;
            auctionProductUser.IsSubmitted = true;
            Uow.SaveChanges();
        });
    }
}