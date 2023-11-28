using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpPost]
        public ActionResult Unmake([FromBody]UnmakeReductionArgument argument) => Execute(() =>
        {
            var product = Uow.Products
                .Include(x=>x.User)
                .FirstOrError(x => x.Id == argument.ProductId);
            var auctionProduct =  Uow.ActualAuctionProductsWithOrdering.FirstOrError(x=>x.ProductId == product.Id,
                "Товар не является тендерным");
            auctionProduct.Deactivate();
            Uow.SaveChanges();
        });
    }
}