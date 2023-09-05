using MMABackend.DomainModels.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void MarketShopPointsSeeding(this UnitOfWork uow) => uow.Execute(
            new ShopPoint
            {
                Id = 1,
                ShopId = InMarketShopIds.fifth.ToInt(),
                Latitude = 42.86557690910285M,
                Longitude = 74.5717280477489M,
            },
            new ShopPoint
            {
                Id = 2,
                ShopId = InMarketShopIds.fifth.ToInt(),
                Latitude = 42.86558477282128M,
                Longitude = 74.57181052568033M,
            },
            new ShopPoint
            {
                Id = 3,
                ShopId = InMarketShopIds.fifth.ToInt(),
                Latitude = 42.86552628639157M,
                Longitude = 74.57183533611499M,
            },
            new ShopPoint
            {
                Id = 4,
                ShopId = InMarketShopIds.fifth.ToInt(),
                Latitude = 42.865508593006965M,
                Longitude = 74.5717568814973M,
            }
        );
    }
}