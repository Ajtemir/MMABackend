using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class GroupDiscountController
    {
        [HttpPost]
        public ActionResult MakeProductGroupDiscount([FromBody]MakeProductGroupDiscountArgument argument) => Execute( () =>
        {
            var user = _uow.GetUserByEmailOrError(argument.SellerEmail);
            var product = _uow.Products.FirstOrError(x=>x.Id == argument.ProductId, 
                $"Не найден продукт по указанному идентификатору: {argument.ProductId}");
            
            if (product.IsNotSeller(user))
                throw new ApplicationException("Вы не являетесь продавцом указанного товара");

            _uow.GroupDiscountProducts.ErrorIfExists(x => x.ProductId == argument.ProductId && x.IsActual.Value,
                "У товара не может быть две коллективной сделки");
            
            _uow.GroupDiscountProducts.Add(new GroupDiscountProduct
            {
                GroupDiscountPrice = argument.GroupDiscountPrice,
                ProductId = product.Id,
                StartDate = argument.StartDate,
                EndDate = argument.EndDate,
                BuyerMinAmount = argument.BuyerAmount,
            });
            _uow.SaveChanges();
        });
    }

    public class MakeProductGroupDiscountArgument
    {
        public int ProductId { get; set; }
        public string SellerEmail { get; set; }
        public decimal GroupDiscountPrice { get; set; }
        public int BuyerAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}