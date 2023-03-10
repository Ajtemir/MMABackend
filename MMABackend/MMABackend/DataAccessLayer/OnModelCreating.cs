using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<ProductProperty>()
            //     .HasKey(x => new { x.ProductId, x.PropertyValueId });
            //
            // modelBuilder.Entity<ProductProperty>()
            //     .HasOne(x => x.Product)
            //     .WithMany(x => x.ProductProperties)
            //     .HasForeignKey(x => x.ProductId)
            //     .IsRequired()
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // modelBuilder.Entity<ProductProperty>()
            //     .HasOne(x => x.PropertyValue)
            //     .WithMany(x => x.ProductProperties)
            //     .HasForeignKey(x => x.PropertyValueId)
            //     .IsRequired()
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            //
            // modelBuilder.Entity<Favorite>()
            //     .HasOne(x => x.Product)
            //     .WithMany(x => x.Favorites)
            //     .HasForeignKey(x => x.ProductId)
            //     .OnDelete(DeleteBehavior.SetNull);
            //
            // modelBuilder.Entity<Favorite>()
            //     .HasOne(x => x.User)
            //     .WithMany(x => x.Favorites)
            //     .HasForeignKey(x => x.UserId);
            //
            // modelBuilder.Entity<Favorite>().HasIndex(x => new { x.UserId, x.ProductId }).IsUnique();
        }
    }
}