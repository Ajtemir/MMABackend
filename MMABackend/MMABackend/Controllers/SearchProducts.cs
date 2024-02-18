using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost]
        public ActionResult Search([FromBody]SearchArgument argument) =>  Execute(() =>
        {
            var dict = argument.PropertyFilters.GroupBy(x => x.IsMultiOrNumberDefault).ToDictionary(x=>x.Key, x=>x.ToList());
            var singleAndMultiValues = dict[false].Select(x => x.SingleValues).ToList();
            dict[true].ForEach(x => singleAndMultiValues.AddRange(x.MultiValues));
            
            var products = _uow.Products
                .Include(x => 
                    x.ProductProperties.Where(p => singleAndMultiValues.Contains(p.PropertyValueId.Value))
                        .Where(p => dict[null].Any(nv => 
                            p.PropertyKeyId == nv.PropertyId && nv.StartValue.Value <= p.NumberValue && p.NumberValue <= nv.EndValue.Value )
                        )
                )
                .Where(x =>
                    (argument.Description == null || x.Description.Contains(argument.Description)) &&
                    ((argument.StartPrice == null && argument.EndPrice == null) || x.Price == null || argument.StartPrice <= x.Price && x.Price <= argument.EndPrice) &&
                    (argument.CategoryId == null || x.CategoryId == argument.CategoryId)
                )
                .ToList();
             return products;
        });
    }

    public class SearchArgument
    {
        public string Description { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public int? CategoryId { get; set; }
        public PropertyFilter[] PropertyFilters { get; set; }
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