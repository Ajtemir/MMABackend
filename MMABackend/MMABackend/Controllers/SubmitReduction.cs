using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpPost]
        public ActionResult Submit(ArgumentSubmitReduction argument) => Execute(() =>
        {
            var product = Uow.Products.FirstOrError(x => x.Id == argument.ProductId);
            product.ValidateSellerById(UserId);
            var auctionProduct = Uow.ActualReductionProductsWithOrdering
                .Include(x=>x.AuctionProductsUsers)
                .FirstOrError(x=>x.ProductId == product.Id,
                    "Тендерный товар не найден");
            var auctionProductUser = auctionProduct.AuctionProductsUsers.OrderBy(x => x.Price)
                .FirstOrError(errorMessage : "У товара нет желающих покупателей");
            auctionProduct.Status = AuctionProductStatus.Submitted;
            auctionProductUser.IsSubmitted = true;
            Uow.SaveChanges();
        });
    }
}