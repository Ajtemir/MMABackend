using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class GroupDiscountController
    {
        [HttpPost]
        public ActionResult AddGroupDiscountProduct(AddCollectiveProductArgument argument) => Execute(() =>
        {
            var buyerId = _uow.GetUserByEmailOrError(argument.BuyerEmail).Id;
            var product = _uow.GroupDiscountProducts
                .FirstOrError(x => x.ProductId == argument.ProductId && x.IsActual.Value);
            _uow.CollectivePurchasers.ErrorIfExists(x =>
                x.GroupDiscountProductId == product.Id && x.BuyerId == buyerId, "Уже есть оказся");
            _uow.CollectivePurchasers.Add(new GroupDiscountProductBuyer
            {
                GroupDiscountProductId = product.Id,
                BuyerId = buyerId,
            });
            _uow.SaveChanges();
            
            return new CollectiveProductResult
            {
                CurrentBuyerCount = _uow.CollectivePurchasers.Count(x => x.GroupDiscountProductId == product.Id),
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