using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMABackend.Controllers;

namespace MMABackend.DataAccessLayer.Extensions
{
    public static partial class DbContextExtensions
    {
        public static List<Property> GetProductProperties(this UnitOfWork uow, int productId)
        {
            var product = uow.Products
                .Include(x => x.ProductProperties)
                .ThenInclude(x => x.PropertyKey)
                .ThenInclude(x => x.PropertyValues)
                .FirstOrDefault(x=>x.Id == productId);
            if (product == null)
                throw new Exception("Not found product");
            var propertyKeys = uow.CategoryPropertyKeys
                .Include(x => x.PropertyKey)
                .ThenInclude(x => x.PropertyValues)
                .Where(x => x.CategoryId == product.CategoryId)
                .Select(x=>new Property
                {
                    Id = x.PropertyKey.Id,
                    IsMultipleOrLiteralDefault = x.PropertyKey.IsMultipleOrLiteralDefault,
                    PropertyValues = x.PropertyKey.PropertyValues,
                    Name = x.PropertyKey.Name,
                })
                .ToList();

            foreach (var group in product.ProductProperties.GroupBy(x=>x.PropertyKeyId))
            {
                var propertyKey = propertyKeys.FirstOrDefault(x => x.Id == group.Key);
                if(propertyKey == null) continue;
                switch (propertyKey.IsMultipleOrLiteralDefault)
                {
                    case true:
                        foreach (var productProperty in group)
                        {
                            if (productProperty?.PropertyValueId != null)
                            {
                                propertyKey.CurrentMultiValues.Add(productProperty.PropertyValueId.Value);
                            }
                        }
                        break;
                    
                    case false:
                        propertyKey.CurrentSingleValue = group.FirstOrDefault()?.PropertyValueId;
                        break;
                    
                    case null:
                        propertyKey.CurrentNumberValue = group.FirstOrDefault()?.NumberValue;
                        break;
                }
            }
            return propertyKeys;
        }
    }
}