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
            uow.ProductImageSeeding();
        }

        private static void ProductSeeding(this UnitOfWork uow)
        {
            uow.Products.AddRange(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина 1",
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина 2",
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 1,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина 3",
                },
                new Product
                {
                    Id = 4,
                    CategoryId = 1,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина 4",
                }
                
            );
            uow.SaveChangesWithIdentityInsert<Product>();
        }

        private static void ProductImageSeeding(this UnitOfWork uow)
        {
            var folderName = "/images/";
            uow.ProductPhotos.AddRange(
                new ProductPhoto
                {
                    Id = 1,
                    ProductId = 1,
                    Path = folderName + "23371fa9-7014-45e6-9887-e468fb57bc9c.png",
                },
                new ProductPhoto
                {
                    Id = 2,
                    ProductId = 2,
                    Path = folderName + "8f5e3f2e-40dc-4552-839f-85e4252a4b84.jpg",
                },
                new ProductPhoto
                {
                    Id = 3,
                    ProductId = 3,
                    Path = folderName + "ac9c793e-65b9-4109-b797-eb908b2f8c6e.png",
                },
                new ProductPhoto
                {
                    Id = 4,
                    ProductId = 4,
                    Path = folderName + "fa86dbbd-b761-442d-9087-1f7d18ecf48c.png",
                }
            );
            uow.SaveChangesWithIdentityInsert<ProductPhoto>();
        }
    }
}