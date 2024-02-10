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
        public ActionResult GetCategoryPropertiesAndTheirValues(int categoryId) => 
            Execute(() => (CategoryPropertiesAndTheirValuesResult) _uow.GetCategoryPropertyAndValuesById(categoryId));
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
                PropertyKeys = category.CategoryPropertyKeys.Select(x=> 
                    new PropertyKeyRecord(
                        x.PropertyKey.IsMultipleOrLiteralDefault,
                        x.PropertyKey.Id,
                        x.PropertyKey.Name,
                        x.PropertyKey.PropertyValues.Select(p=>new PropertyKeyValueRecord(p.Id, p.Name)).ToArray()
                    )).ToList(),
            };
        }
    }
    public record PropertyKeyRecord(bool? IsMultiple, int Id, string Name, PropertyKeyValueRecord[] PropertyKeys);
    public record PropertyKeyValueRecord(int Id, string Name);
}