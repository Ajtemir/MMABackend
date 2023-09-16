using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        public ActionResult UnmakeAuction(ArgumentUnmakeAuction argument) => Execute(() =>
        {
            var user = Uow.GetUserByEmailOrError(argument.SellerEmail);
            var product = Uow.Products.FirstOrError(x => x.Id == argument.ProductId);
            product.ValidateSeller(user);
            var auctionProduct = Uow.AuctionProducts.FirstOrError(x=>x.ProductId == product.Id && x.IsActive.Value,
                "Товар не является аукционным");
            auctionProduct.Deactivate();
            Uow.SaveChanges();
        });
    }
}