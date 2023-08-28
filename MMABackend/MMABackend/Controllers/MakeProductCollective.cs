using System;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController
    {
        [HttpPost]
        public ActionResult MakeProductCollective([FromBody]MakeProductCollectiveArgument argument) => Execute(async () =>
        {
            var userId = _uow.GetUserIdByEmailOrError(argument.SellerEmail);
            var productId = _uow.Products.FirstOrError(x=>x.Id == argument.ProductId, 
                $"Не найден продукт по указанному идентификатору: {argument.ProductId}").Id;
            _uow.CollectiveSoldProducts.Add(new CollectiveSoldProduct
            {
                CollectivePrice = argument.CollectivePrice,
                ProductId = productId,
                StartDate = argument.StartDate,
                EndDate = argument.EndDate,
                BuyerMinAmount = argument.BuyerAmount,
            });
            await _uow.SaveChangesAsync();
        });
    }

    public class MakeProductCollectiveArgument
    {
        public int ProductId { get; set; }
        public string SellerEmail { get; set; }
        public decimal CollectivePrice { get; set; }
        public int BuyerAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}