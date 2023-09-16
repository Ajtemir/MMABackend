using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class AuctionController
    {
        public ActionResult Apply(ArgumentApply argument) => Execute(() =>
        {
            var auctionProduct = Uow.AuctionProducts.FirstOrError(x => x.IsActive.Value && x.ProductId == argument.ProductId, 
                "Указанный товар не является аукционным");
            Uow.AuctionProductUsers.Add(new AuctionProductUser
            {
                Price = argument.SuggestedPrice,
                ProductId = argument.ProductId,
                UserId = argument.BuyerEmail,
            });
            Uow.SaveChanges();
        });
    }
}