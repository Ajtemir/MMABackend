using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult GetProductsByCategoryId(int categoryId) => 
            Execute(() => _uow.Products
                .Include(x=>x.Photos)
                .Where(x=>x.CategoryId == categoryId)
                .Select(x=>(ReadProductViewModel)x).ToList());
    }
}