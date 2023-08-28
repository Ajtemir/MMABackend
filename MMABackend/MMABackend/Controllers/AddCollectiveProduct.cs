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
            _uow.CollectiveSoldProducts.AsNoTracking().FirstOrError(x => x.ProductId == argument.ProductId && x.IsActual.Value);
            _uow.CollectivePurchasers.Add(new CollectivePurchaser
            {
                CollectiveSoldProductId = argument.ProductId,
                BuyerId = buyerId,
            });
            _uow.SaveChanges();
        });
    }

    public class AddCollectiveProductArgument
    {
        public int ProductId { get; set; }
        public string BuyerEmail { get; set; }
    }
}