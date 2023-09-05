using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.Controllers
{
    public partial class MarketController
    {
        [HttpGet]
        public ActionResult GetShopsByMarketId([FromQuery]GetShopsByMarketIdArgument argument) => Execute(() =>
        {
            return _uow.Shops.Include(x => x.ShopLocationDetail)
                .ThenInclude(x=>x.ShopPoints)
                .Where(x => x.ShopLocationDetail.MarketId == argument.MarketId)
                .Select(x =>new 
                {
                    x.Id,
                    Points = x.ShopLocationDetail.ShopPoints,
                })
                .ToList();
        });
    }

    public class GetShopsByMarketIdArgument
    {
        public int MarketId { get; set; }
    }
}