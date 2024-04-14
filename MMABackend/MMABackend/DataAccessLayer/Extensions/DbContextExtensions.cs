using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMABackend.Controllers;

namespace MMABackend.DataAccessLayer.Extensions
{
    public static class DbContextExtensions
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
            var properties = product.ProductProperties.GroupBy(x => x.PropertyKey)
                .Select(group =>
                {
                    var keyProperty = new Property
                    {
                        Id = group.Key.Id,
                        IsMultipleOrLiteralDefault = group.Key.IsMultipleOrLiteralDefault,
                        PropertyValues = group.Key.PropertyValues,
                        Name = group.Key.Name,
                    };
                    switch (group.Key.IsMultipleOrLiteralDefault)
                    {
                        case true:
                            foreach (var productProperty in group)
                            {
                                if (productProperty?.PropertyValueId != null)
                                {
                                    keyProperty.CurrentMultiValues.Add(productProperty.PropertyValueId.Value);
                                }
                            }
                            break;
                    
                        case false:
                            keyProperty.CurrentSingleValue = group.FirstOrDefault()?.PropertyValueId;
                            break;
                    
                        case null:
                            keyProperty.CurrentNumberValue = group.FirstOrDefault()?.NumberValue;
                            break;
                    }
                    return keyProperty;
                }).ToList();
            return properties;
        }
    }
}