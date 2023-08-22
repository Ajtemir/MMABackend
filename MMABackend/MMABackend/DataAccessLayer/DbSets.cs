using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<PropertyKey> PropertyKeys { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<ShopLocationDetail> ShopLocationDetails { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
        public DbSet<CollectivePurchaser> CollectivePurchasers { get; set; }
        public DbSet<CollectiveSoldProduct> CollectiveSoldProducts { get; set; }


    }
}