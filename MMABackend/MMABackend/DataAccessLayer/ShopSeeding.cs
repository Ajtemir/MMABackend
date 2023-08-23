using System.Linq;
using MMABackend.DomainModels.Common;
using MMABackend.Enums.Common;
using MMABackend.Helpers.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void ShopSeeding(this UnitOfWork uow)
        {
            uow.Execute(
                new Shop
                {
                    Id = ShopIds.first.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.first.ToInt())),
                    ShopType = ShopType.Market,
                },
                new Shop
                {
                    Id = ShopIds.second.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.second.ToInt())),
                    ShopType = ShopType.Fixed,
                },
                new Shop
                {
                    Id = ShopIds.third.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.third.ToInt())),
                    ShopType = ShopType.Free,
                },
                new Shop
                {
                    Id = ShopIds.fourth.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.fourth.ToInt())),
                    ShopType = ShopType.Online,
                }
            );
        }
        
        
    }

    enum ShopIds
    {
        first = 1,
        second = 2,
        third = 3,
        fourth = 4,
    }
}