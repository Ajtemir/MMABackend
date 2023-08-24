using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void CategorySeed(this UnitOfWork uow)
        {
            var folderName = "/categories/";
            uow.Categories.AddRange(
                new Category
                {
                    Id = (int)CategoriesIds.Товары,
                    Name = "Товары",
                    ImagePath = folderName + "809049f5-00cc-41b9-80ee-9f5437897d3f.png",
                },
                new Category
                {
                    Id = (int)CategoriesIds.Транспорт,
                    Name = "Транспорт",
                    ImagePath = folderName + "b86e7369-e6ab-42bb-bb09-d79c8ed85530.png",
                },
                new Category
                {
                    Id = (int)CategoriesIds.Домашняя_утварь,
                    Name = "Домашняя утварь",
                    ImagePath = folderName + "cat1.png",
                },
                new Category
                {
                    Id = (int)CategoriesIds.Автомобиль,
                    Name = "Автомобиль",
                    ImagePath = folderName + "cat9.png",
                    ParentCategoryId = (int)CategoriesIds.Транспорт,
                }
            );
            uow.SaveChangesWithIdentityInsert<Category>();
        }
    }
}