using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController
    {
        [HttpDelete]
        public ActionResult RemoveCollectiveProduct(RemoveCollectiveProductArgument argument) => Execute(() =>
        {
            var buyer = _uow.GetUserByEmailOrError(argument.BuyerEmail);
            var product = _uow.CollectiveSoldProducts.FirstOrError(x => x.ProductId == argument.ProductId);
            var collectiveSoldProduct = _uow.CollectivePurchasers.FirstOrError(x => 
                x.CollectiveSoldProductId == product.Id && x.BuyerId == buyer.Id);
            _uow.CollectivePurchasers.Remove(collectiveSoldProduct);
            _uow.SaveChanges();
            return new CollectiveProductResult
            {
                CurrentBuyerCount = _uow.CollectivePurchasers.Count(x=>x.CollectiveSoldProductId == product.Id),
            };
        });
    }

    public class RemoveCollectiveProductArgument
    {
        public string BuyerEmail { get; set; }
        public int ProductId { get; set; }
    }
}