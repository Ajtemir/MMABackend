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
            
            IEnumerable<Product> productsFilteredByMainData = _uow.Products
                .Include(x => x.Photos).Include(product => product.ProductProperties)
                .Where(x =>
                    ((argument.StartPrice == null && argument.EndPrice == null) || x.Price == null || argument.StartPrice <= x.Price && x.Price <= argument.EndPrice) &&
                    (argument.CategoryId == null || x.CategoryId == argument.CategoryId)
                ).ToList();
            productsFilteredByMainData = productsFilteredByMainData.Where(x =>
                (argument.Description == null ||
                 x.Description.Contains(argument.Description, StringComparison.InvariantCultureIgnoreCase)));

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

            if (argument.OrderByDate.HasValue)
            {
                productsFilteredByMainData = argument.OrderByDate switch
                {
                    OrderByDate.DateAsc => productsFilteredByMainData.OrderBy(x => x.CreatedDate),
                    OrderByDate.DateDesc => productsFilteredByMainData.OrderByDescending(x => x.CreatedDate),
                    _ => productsFilteredByMainData
                };
            }

            if (argument.OrderByPrice.HasValue)
            {
                productsFilteredByMainData = argument.OrderByPrice switch
                {
                    OrderByPrice.PriceAsc => productsFilteredByMainData.OrderBy(x => x.Price),
                    OrderByPrice.PriceDesc => productsFilteredByMainData.OrderByDescending(x => x.Price),
                    _ => productsFilteredByMainData
                };
                
            }

            var result = productsFilteredByMainData.ToPagedList(argument.PageNumber,argument.PageSize);
            
            return result;
        });
    }

    public class SearchArgument : IPagination
    {
        public string Description { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public int? CategoryId { get; set; }
        public PropertyFilter[] PropertyFilters { get; set; } = Array.Empty<PropertyFilter>();
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public OrderByPrice? OrderByPrice { get; set; }
        public OrderByDate? OrderByDate { get; set; }
    }

    public enum OrderByPrice
    {
        PriceAsc = 0,
        PriceDesc,
        
    }

    public enum OrderByDate
    {
        DateAsc = 0,
        DateDesc,
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

    public interface IPagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public static class Paginate
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
    
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
    }
}