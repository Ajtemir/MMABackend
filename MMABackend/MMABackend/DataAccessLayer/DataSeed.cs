using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.DataAccessLayer
{
    public static class DataSeeding
    {
        private static void CategorySeed(this UnitOfWork uow)
        {
            uow.Categories.AddRange(
                        new Category
                        {
                            Id = 1,
                            Name = "Автомобили",
                            ParentCategoryId = null,
                        },
                        new Category
                        {
                            Id = 2,
                            Name = "Одежда",
                            ParentCategoryId = null,
                        }
            );
            uow.SaveChangesWithIdentityInsert<Category>();
        }
        public static void CommonSeeding(this IServiceProvider serviceProvider)
        {
            var uow = serviceProvider?.GetService<UnitOfWork>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            uow.CategorySeed();
            uow.ProductSeeding();
        }

        private static void ProductSeeding(this UnitOfWork uow)
        {
            uow.Products.AddRange(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина",
                }
            );
            uow.SaveChangesWithIdentityInsert<Product>();
        }
    }
}