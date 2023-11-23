using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DataAccessLayer;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpPost]
        public ActionResult Deny([FromBody] DenyReductionArgument argument) => Execute(() =>
        {
            var auctionProduct = Uow.ActualAuctionProductsWithOrdering.FirstOrError(x=>x.ProductId == argument.ProductId, 
                "Товар не является тендерным чтобы убрать из тендера");
            var auctionProductUser = Uow.AuctionProductUsers.FirstOrError(x =>  
                    x.AuctionProductId == auctionProduct.Id &&
                    x.UserId == UserId,
                "Вы не подавались к тендерной покупке");
            Uow.Remove(auctionProductUser);
        });
    }
}