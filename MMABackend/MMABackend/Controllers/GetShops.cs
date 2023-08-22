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
            return _uow.ShopLocationDetails.Include(x => x.Market)
                .Select(x => new
                {
                    Latitude = x.ShopType == ShopType.Market ? x.Latitude : x.Market.Latitude,
                    Longtitude = x.ShopType == ShopType.Market ? x.Longitude : x.Market.Longitude,
                    x.ShopType,
                    x.Id,
                    IsInMarket = x.ShopType == ShopType.Market,
                });
        });
    }

    public class GetShopArgument
    {
        
    }
}