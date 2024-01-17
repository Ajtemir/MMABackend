using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CategoriesController
    {
        
        [HttpGet]
        public ActionResult<Category> GetCategoryById([FromQuery]GetCategoryByIdArgument argument) => Execute(() =>
        {
            var category = _uow.Categories.Include(x => x.SubCategories).ThenInclude(x=>x.SubCategories)
                .FirstOrError(x => x.Id == argument.categoryId, "Не найдена искомая категория");
            return category;
        });
        
        public class GetCategoryByIdArgument
        {
            public int categoryId { get; set; }
        }
    }
}