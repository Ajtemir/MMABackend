using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MMABackend.Controllers
{
    public partial class MarketController
    {
        [HttpGet("{marketId:int}")]
        public ActionResult GetShopsByMarketId(int marketId) => Execute(() =>
        {
            return _uow.Shops.Include(x => x.ShopLocationDetail)
                .Where(x => x.ShopLocationDetail.MarketId == marketId)
                .ToList();
        });
    }
}