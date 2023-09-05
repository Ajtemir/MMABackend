using MMABackend.DomainModels.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void ShopLocationDetailSeeding(this UnitOfWork uow)
        {
            uow.Execute(new ShopLocationDetail
                {
                    MarketId = MarketIds.Usta.ToInt(),
                    ShopId = ShopIds.first.ToInt(),
                },
                new ShopLocationDetail
                {
                    ShopId = ShopIds.second.ToInt(),
                    Latitude = 42.84006204227845M,
                    Longitude = 74.58459577306591M,
                },
                new ShopLocationDetail
                {
                    ShopId = ShopIds.third.ToInt(),
                    Latitude = 42.829126104728196M,
                    Longitude = 74.58496639213371M,
                },
                new ShopLocationDetail
                {
                    ShopId = InMarketShopIds.fifth.ToInt(),
                    MarketId = MarketIds.Usta.ToInt(),
                }
            );
        }
    }
}