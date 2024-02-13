using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost]
        public ActionResult<Product> AddWithEmail(AddProductWithEmailViewModel model) => Execute(() =>
        {
            var user = _uow.GetUserByEmailOrError(model.UserEmail ?? model.UserId);
            Product product = model;
            product.UserId = user.Id;
            _uow.Products.Add(product);
            _uow.SaveChanges();
            var category = _uow.Categories.Include(x => x.CategoryPropertyKeys).FirstOrError(x => x.Id == product.CategoryId);
            _uow.ProductProperties.AddRange(category.CategoryPropertyKeys.Select(x=>new ProductProperty
            {
                ProductId = product.Id,
                PropertyKeyId = x.Id,
            }));
            _uow.SaveChanges();
            return (ReadProductViewModel)product;
        });
    }
}