using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost]
        public ActionResult Search([FromBody]SearchArgument argument) =>  Execute(() =>
        {
            var singleAndMultiValues = new List<int>();
            foreach (var multiFilter in argument.PropertyFilters.Where(x=>x.IsMultiOrNumberDefault != null && x.IsMultiOrNumberDefault.Value))
            {
                singleAndMultiValues.AddRange(multiFilter.MultiValues);
            }
            foreach (var multiFilter in argument.PropertyFilters.Where(x=>x.IsMultiOrNumberDefault != null && !x.IsMultiOrNumberDefault.Value))
            {
                singleAndMultiValues.Add(multiFilter.SingleValues);
            }
            
            var productsFilteredByMainData = _uow.Products
                .Include(x=>x.ProductProperties)
                .Where(x =>
                    (argument.Description == null || x.Description.Contains(argument.Description)) &&
                    ((argument.StartPrice == null && argument.EndPrice == null) || x.Price == null || argument.StartPrice <= x.Price && x.Price <= argument.EndPrice) &&
                    (argument.CategoryId == null || x.CategoryId == argument.CategoryId)
                ).ToList();

            if (singleAndMultiValues.Any())
            {
                productsFilteredByMainData = productsFilteredByMainData
                    .Where(x => x.ProductProperties.Select(z => z.PropertyValueId ?? 0).Any(y => singleAndMultiValues.Contains(y)))
                    .ToList();
            }
            

            var rangeProperties = argument.PropertyFilters.Where(x => x.IsMultiOrNumberDefault == null).ToList();
            if (rangeProperties.Any())
            {
                productsFilteredByMainData = productsFilteredByMainData.Where(p => p.ProductProperties.Any(pp =>
                    rangeProperties
                        .Any(rp => pp.PropertyKeyId == rp.PropertyId &&
                                   (rp.StartValue == null || rp.StartValue <= pp.NumberValue) &&
                                   (rp.EndValue == null || pp.NumberValue <= rp.EndValue))
                )).ToList();
            }
            
            return productsFilteredByMainData;
        });
    }

    public class SearchArgument
    {
        public string Description { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public int? CategoryId { get; set; }
        public PropertyFilter[] PropertyFilters { get; set; } = Array.Empty<PropertyFilter>();
    }

    public class PropertyFilter
    {
        public bool? IsMultiOrNumberDefault { get; set; }
        public int PropertyId { get; set; }
        public int[] MultiValues { get; set; }
        public int SingleValues { get; set; }
        public int? StartValue { get; set; }
        public int? EndValue { get; set; }
    }
}