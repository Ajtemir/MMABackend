using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost]
        public ActionResult Update([FromBody]UpdateProductArgument argument) => Execute(() =>
        {
            var product = _uow.Products.FirstOrError(x=>x.Id == argument.ProductId);
            product.Title = argument.Title;
            product.Description = argument.Description;
            product.Price = argument.Price;
            _uow.SaveChanges();
        });
    }

    public class UpdateProductArgument
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        
    }
    
}