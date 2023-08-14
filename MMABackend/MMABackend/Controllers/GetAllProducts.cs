using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<List<Product>> Index(string email = null, bool isNew = false) => Execute(() =>
        {
            var entities = _uow.Products
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Photos)
                .Where(x => x.User.Email == email || email == null);
            if (isNew) entities = entities.OrderByDescending(x => x.CreatedDate);
            List<ReadProductViewModel> result = entities.Select(x => (ReadProductViewModel)x).ToList();
            return result;
        });
    }
}