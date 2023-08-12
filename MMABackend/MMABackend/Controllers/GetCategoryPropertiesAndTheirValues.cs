using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
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
                return Result.Ok((CategoryPropertiesAndTheirValuesResult)propertyKeyAndValuesOfCategory);
            });
        }
    }

    public class CategoryPropertiesAndTheirValuesResult
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<PropertyKeyRecord> PropertyKeys { get; set; } = new ();
        public static explicit operator CategoryPropertiesAndTheirValuesResult(Category category)
        {
            return new CategoryPropertiesAndTheirValuesResult
            {
                CategoryName = category.Name,
                CategoryId = category.Id,
                PropertyKeys = category.PropertyKeys.Select(x=> 
                    new PropertyKeyRecord(
                        x.IsMultiple,
                        x.Id,
                        x.Name,
                        x.PropertyValues.Select(p=>new PropertyKeyValueRecord(p.Id, p.Name)).ToArray()
                    )).ToList(),
            };
        }
    }
    public record PropertyKeyRecord(bool IsMultiple, int Id, string Name, PropertyKeyValueRecord[] PropertyKeys);
    public record PropertyKeyValueRecord(int Id, string Name);
}