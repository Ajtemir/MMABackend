using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;

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
                    .FirstOrError(x => x.Id == categoryId);
                return propertyKeyAndValuesOfCategory;
            });
        }
    }
    
    public class CategoryPropertiesAndTheirValuesResult()
}