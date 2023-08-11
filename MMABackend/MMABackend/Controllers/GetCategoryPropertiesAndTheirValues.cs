using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.Controllers
{
    public partial class CategoriesController
    {
        
        [HttpGet]
        public ActionResult GetCategoryPropertiesAndTheirValues(int categoryId)
        {
            return Execute(() =>
            {
                var propertyKeyAndValuesOfCategory = _uow.Categories
                    .Include(x => x.PropertyKeys)
                    .ThenInclude(x => x.PropertyValues)
                    .FirstOrDefault(x => x.Id == categoryId);
                return propertyKeyAndValuesOfCategory;
            });
        }
    }
}