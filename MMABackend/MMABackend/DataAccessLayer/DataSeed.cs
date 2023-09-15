using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.Utilities.Extensions;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        public static void CommonSeeding(this IServiceProvider serviceProvider)
        {
            var uow = serviceProvider?.GetService<UnitOfWork>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            uow.MarketSeeding();
            uow.ShopSeeding();
            uow.ShopLocationDetailSeeding();
            uow.CategorySeed();
            uow.ProductSeeding();
            uow.ProductImageSeeding();
            uow.ProductPropertySeed();
            uow.ProductValueSeed();
            uow.ProductPropertyValueSeeding();
            uow.FavoritesSeeding();
            uow.MakeCollectiveSeeding();
            uow.MarketShopPointsSeeding();
        }

        private static void ProductPropertySeed(this UnitOfWork uow)
        {
            uow.PropertyKeys.AddRange(
                new PropertyKey
                {
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    Name = "Руль",
                    Id = (int)PropertyKeyIds.Руль,
                },
                new PropertyKey
                {
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    Name = "Топливо",
                    Id = (int)PropertyKeyIds.Топливо,
                    IsMultiple = true,
                }
                );
            uow.SaveChangesWithIdentityInsert<PropertyKey>();
        }
        
        private static void ProductValueSeed(this UnitOfWork uow)
        {
            uow.PropertyValues.AddRange(
                new PropertyValue
                {
                    PropertyKeyId = (int)PropertyKeyIds.Руль,
                    Name = "Правый",
                    Id = 1,
                },
                new PropertyValue
                {
                    PropertyKeyId = (int)PropertyKeyIds.Руль,
                    Name = "Левый",
                    Id = 2,
                },
                
                new PropertyValue
                {
                    PropertyKeyId = (int)PropertyKeyIds.Топливо,
                    Name = "Бензин",
                    Id = 3,
                },
                new PropertyValue
                {
                    PropertyKeyId = (int)PropertyKeyIds.Топливо,
                    Name = "Дизель",
                    Id = 4,
                }
            );
            uow.SaveChangesWithIdentityInsert<PropertyValue>();
        }
        
        private static void ProductSeeding(this UnitOfWork uow)
        {
            uow.Products.AddRange(
                new Product
                {
                    Id = 1,
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    ShopId = ShopIds.first.ToInt(),
                    Description = @"Машина 1 name Так как на данный момент в семье 2 универсала один моя Toyota Caldina в комплектации Twister 2 литра с двигателем 3S-FE 2002 года и братовская Toyota Camry Gracia 2.2 литра с двигателем 5S-FE 2001 года то буду иногда в чем-то их сравнивать.

                    Продолжение отзыва будет разделено на пункты для удобства и простоты чтения.

                    Комплектация:

                    - Двигатель простой 3S-FE не D4;

                    - Спойлер;

                    - Обвес по кругу, дорожный просвет не съедает и смотрится симпатичней;",
                },
                new Product
                {
                    Id = 2,
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    ShopId = ShopIds.first.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(UserIds.second.ToInt())),
                    Description = "Машина 2",
                },
                new Product
                {
                    Id = 3,
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    ShopId = ShopIds.first.ToInt(),
                    UserId = uow.GetUserIdByEmailOrError(SampleData.GetEmailByIndex(1)),
                    Description = "Машина 3",
                },
                new Product
                {
                    Id = 4,
                    CategoryId = (int)CategoriesIds.Автомобиль,
                    ShopId = ShopIds.first.ToInt(),
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
        
        private static void ProductPropertyValueSeeding(this UnitOfWork uow)
        {
            uow.ProductProperties.AddRange(
                new ProductProperty
                {
                    Id = 1,
                    ProductId = 1,
                    PropertyValueId = 1,
                },
                new ProductProperty
                {
                    Id = 2,
                    ProductId = 1,
                    PropertyValueId = 3,
                },
                new ProductProperty
                {
                    Id = 3,
                    ProductId = 1,
                    PropertyValueId = 4,
                }
                );
            uow.SaveChangesWithIdentityInsert<ProductProperty>();
        }
    }

    public enum CategoriesIds
    {
        Товары = 1,
        Транспорт = 2,
        Домашняя_утварь = 3,
        Автомобиль = 4,
    }

    public enum PropertyKeyIds
    {
        Руль = 1,
        Топливо = 2,
    }
}