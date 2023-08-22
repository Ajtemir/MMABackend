using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Enums.Common;

namespace MMABackend.Controllers
{
    public partial class ShopsController
    {
        [HttpGet]
        public ActionResult GetShops([FromQuery] GetShopArgument argument) => Execute(() =>
        {
            var shops = _uow.ShopLocationDetails.Include(x => x.Market)
                .Where(x => x.ShopType == ShopType.Fixed || x.ShopType == ShopType.Free)
                .Select(x => new
                {
                    Latitude = (decimal)x.Latitude,
                    Longitude = (decimal)x.Longitude,
                    x.ShopType,
                    x.Id,
                }).ToList();
            
            var markets = _uow.Markets.Select(x => new
            {
                x.Latitude,
                x.Longitude,
                ShopType = ShopType.Market,
                x.Id,
            }).ToList();

            var all = shops.Union(markets);
            return all;
        });
    }

    public class GetShopArgument
    {
        
    }
}