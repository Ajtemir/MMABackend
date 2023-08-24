using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void FavoritesSeeding(this UnitOfWork uow) => uow.Execute(
            new Favorite
            {
                Id = FavoritesIds.first.ToInt(),
                UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.first.ToInt())),
                ProductId = 2,
            }
        );
    }

    public enum FavoritesIds
    {
        first = 1,
        second = 2,
        third = 3,
        
    }
}