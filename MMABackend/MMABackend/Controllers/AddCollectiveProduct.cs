using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController
    {
        [HttpPost]
        public ActionResult AddCollectiveProduct(AddCollectiveProductArgument argument) => Execute(() =>
        {
            var buyerId = _uow.GetUserByEmailOrError(argument.BuyerEmail).Id;
            var product = _uow.CollectiveSoldProducts
                .FirstOrError(x => x.ProductId == argument.ProductId && x.IsActual.Value);
            _uow.CollectivePurchasers.Add(new CollectivePurchaser
            {
                CollectiveSoldProductId = argument.ProductId,
                BuyerId = buyerId,
            });
            _uow.SaveChanges();
            
            return new CollectiveProductResult
            {
                CurrentBuyerCount = _uow.CollectivePurchasers.Count(x => x.CollectiveSoldProductId == product.Id),
            };
        });
    }

    public class AddCollectiveProductArgument
    {
        public int ProductId { get; set; }
        public string BuyerEmail { get; set; }
    }

    public class CollectiveProductResult
    {
        public int CurrentBuyerCount { get; set; }
    }
}