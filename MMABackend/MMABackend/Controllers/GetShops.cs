using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Enums.Common;

namespace MMABackend.Controllers
{
    public partial class ShopsController
    {
        [HttpGet]
        public ActionResult GetShopsAndMarkets([FromQuery] GetShopArgument argument) => Execute(() =>
        {
            var shops = _uow.Shops.Include(x => x.ShopLocationDetail)
                .Where(x => x.ShopType == ShopType.Fixed || x.ShopType == ShopType.Free)
                .Select(x => new
                {
                    Latitude = (decimal)x.ShopLocationDetail.Latitude,
                    Longitude = (decimal)x.ShopLocationDetail.Longitude,
                    x.ShopType,
                    x.Id,
                    IsMarket = false,
                    x.User.Email,
                }).ToList();
            
            var markets = _uow.Markets.Select(x => new
            {
                x.Latitude,
                x.Longitude,
                ShopType = ShopType.Market,
                x.Id,
                IsMarket = true,
                Email = string.Empty,
            }).ToList();

            var all = shops.Union(markets);
            return all;
        });
    }

    public class GetShopArgument
    {
        
    }
}