using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class GroupDiscountController
    {
        [HttpDelete]
        public ActionResult RemoveGroupDiscountProduct(RemoveGroupDiscountProductArgument argument) => Execute(() =>
        {
            var buyer = _uow.GetUserByEmailOrError(argument.BuyerEmail);
            var product = _uow.GroupDiscountProducts.FirstOrError(x => x.ProductId == argument.ProductId);
            var collectiveSoldProduct = _uow.CollectivePurchasers.FirstOrError(x => 
                x.GroupDiscountProductId == product.Id && x.BuyerId == buyer.Id);
            _uow.CollectivePurchasers.Remove(collectiveSoldProduct);
            _uow.SaveChanges();
            return new CollectiveProductResult
            {
                CurrentBuyerCount = _uow.CollectivePurchasers.Count(x=>x.GroupDiscountProductId == product.Id),
            };
        });
    }

    public class RemoveGroupDiscountProductArgument
    {
        public string BuyerEmail { get; set; }
        public int ProductId { get; set; }
    }
}