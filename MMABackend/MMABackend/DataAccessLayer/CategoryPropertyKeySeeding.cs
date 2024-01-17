using MMABackend.DomainModels.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public partial class DataSeeding
    {
        private static void CategoryPropertyKeySeeding(this UnitOfWork uow) => uow.Execute(
            new CategoryPropertyKey
            {
                Id = 1,
                CategoryId = CategoriesIds.Автомобиль.ToInt(),
                PropertyKeyId = PropertyKeyIds.Пробег.ToInt(),
            },
            new CategoryPropertyKey
            {
                Id = 2,
                CategoryId = CategoriesIds.Автомобиль.ToInt(),
                PropertyKeyId = PropertyKeyIds.Руль.ToInt(),
            },
            new CategoryPropertyKey
            {
                Id = 3,
                CategoryId = CategoriesIds.Автомобиль.ToInt(),
                PropertyKeyId = PropertyKeyIds.Топливо.ToInt(),
            }
        );
    }
}