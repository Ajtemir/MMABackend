using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController
    {
        [HttpPost]
        public ActionResult MakeProductCollective([FromBody]MakeProductCollectiveArgument argument) => Execute( () =>
        {
            var userId = _uow.GetUserIdByEmailOrError(argument.SellerEmail);
            var product = _uow.Products.FirstOrError(x=>x.Id == argument.ProductId, 
                $"Не найден продукт по указанному идентификатору: {argument.ProductId}");
            
            if (product.UserId != userId)
                throw new ApplicationException("Вы не являетесь продавцом указанного товара");
            
            if (_uow.CollectiveSoldProducts.FirstOrDefault(x => 
                    x.ProductId == argument.ProductId && x.IsActual.Value) != null)
                throw new ApplicationException("У товара не может быть две коллективной сделки");
            
            _uow.CollectiveSoldProducts.Add(new CollectiveSoldProduct
            {
                CollectivePrice = argument.CollectivePrice,
                ProductId = product.Id,
                StartDate = argument.StartDate,
                EndDate = argument.EndDate,
                BuyerMinAmount = argument.BuyerAmount,
            });
            _uow.SaveChanges();
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