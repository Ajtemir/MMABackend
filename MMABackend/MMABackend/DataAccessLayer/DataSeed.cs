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
        public static void CommonSeeding(this IServiceProvider serviceProvider)
        {
            var uow = serviceProvider?.GetService<UnitOfWork>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            uow.CategorySeed();
            uow.ProductSeeding();
            uow.ProductImageSeeding();
            
        }
        
        
        private static void CategorySeed(this UnitOfWork uow)
        {
            var folderName = "/images/";
            uow.Categories.AddRange(
                        new Category
                        {
                            Id = 1,
                            Name = "Товары",
                            ImagePath = folderName + "809049f5-00cc-41b9-80ee-9f5437897d3f.png",
                        },
                        new Category
                        {
                            Id = 2,
                            Name = "Транспорт",
                            ImagePath = folderName + "b86e7369-e6ab-42bb-bb09-d79c8ed85530.png",
                        },
                        new Category
                        {
                            Id = 3,
                            Name = "Домашняя утварь",
                            ImagePath = folderName + "b250dcb4-2967-44d6-9295-e154cd43e77e.png",
                        }
            );
            uow.SaveChangesWithIdentityInsert<Category>();
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
                    Path = folderName + "1_photo.jpg",
                },
                new ProductPhoto
                {
                    Id = 2,
                    ProductId = 2,
                    Path = folderName + "2_photo.jpg",
                },
                new ProductPhoto
                {
                    Id = 3,
                    ProductId = 3,
                    Path = folderName + "3_photo.jpg",
                },
                new ProductPhoto
                {
                    Id = 4,
                    ProductId = 4,
                    Path = folderName + "4_photo.jpg",
                }
            );
            uow.SaveChangesWithIdentityInsert<ProductPhoto>();
        }
    }
}