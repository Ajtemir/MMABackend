﻿// <auto-generated />
using System;
using MMABackend.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MMABackend.Migrations
{
    [DbContext(typeof(UnitOfWork))]
    [Migration("20240117112122_literalAdded")]
    partial class literalAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("MMABackend.DomainModels.Common.AuctionProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAuctionElseReduction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("StartPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("AuctionProducts");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.AuctionProductUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuctionProductId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuctionProductId");

                    b.HasIndex("UserId");

                    b.ToTable("AuctionProductUsers");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CategoryPropertyKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyKeyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PropertyKeyId");

                    b.ToTable("CategoryPropertyKeys");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CollectivePurchaser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("CollectiveSoldProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CollectiveSoldProductId");

                    b.HasIndex("BuyerId", "CollectiveSoldProductId")
                        .IsUnique();

                    b.ToTable("CollectivePurchasers");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CollectiveSoldProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BuyerMinAmount")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CollectivePrice")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActual")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId", "IsActual")
                        .IsUnique();

                    b.ToTable("CollectiveSoldProducts");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("Decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("Decimal(9,6)");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ShopId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ProductPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPhotos");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ProductProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LiteralValue")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyKeyId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PropertyValueId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyKeyId");

                    b.HasIndex("PropertyValueId");

                    b.HasIndex("ProductId", "PropertyValueId")
                        .IsUnique();

                    b.ToTable("ProductProperties");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.PropertyKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsMultipleOrLiteralDefault")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PropertyKeys");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.PropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PropertyKeyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyKeyId");

                    b.ToTable("PropertyValues");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShopType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MarketId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ShopLocationDetail", b =>
                {
                    b.Property<int>("ShopId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("Decimal(8,6)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("Decimal(9,6)");

                    b.Property<int?>("MarketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER");

                    b.HasKey("ShopId");

                    b.HasIndex("MarketId");

                    b.ToTable("ShopLocationDetails");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ShopPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("Decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("Decimal(9,6)");

                    b.Property<int>("ShopId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ShopId", "Latitude", "Longitude")
                        .IsUnique();

                    b.ToTable("ShopPoints");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.UserAvatar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UploadTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAvatars");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.AuctionProduct", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Product", "Product")
                        .WithMany("AuctionProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.AuctionProductUser", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.AuctionProduct", "AuctionProduct")
                        .WithMany("AuctionProductsUsers")
                        .HasForeignKey("AuctionProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMABackend.DomainModels.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("AuctionProduct");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Category", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CategoryPropertyKey", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Category", "Category")
                        .WithMany("CategoryPropertyKeys")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMABackend.DomainModels.Common.PropertyKey", "PropertyKey")
                        .WithMany()
                        .HasForeignKey("PropertyKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("PropertyKey");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CollectivePurchaser", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("MMABackend.DomainModels.Common.CollectiveSoldProduct", "CollectiveSoldProduct")
                        .WithMany("CollectivePurchasers")
                        .HasForeignKey("CollectiveSoldProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("CollectiveSoldProduct");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CollectiveSoldProduct", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Product", "Product")
                        .WithMany("CollectiveSoldProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Favorite", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductId");

                    b.HasOne("MMABackend.DomainModels.Common.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Product", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMABackend.DomainModels.Common.Shop", "Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId");

                    b.HasOne("MMABackend.DomainModels.Common.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("Shop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ProductPhoto", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ProductProperty", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Product", "Product")
                        .WithMany("ProductProperties")
                        .HasForeignKey("ProductId");

                    b.HasOne("MMABackend.DomainModels.Common.PropertyKey", "PropertyKey")
                        .WithMany()
                        .HasForeignKey("PropertyKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMABackend.DomainModels.Common.PropertyValue", "PropertyValue")
                        .WithMany("ProductProperties")
                        .HasForeignKey("PropertyValueId");

                    b.Navigation("Product");

                    b.Navigation("PropertyKey");

                    b.Navigation("PropertyValue");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.PropertyValue", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.PropertyKey", "PropertyKey")
                        .WithMany("PropertyValues")
                        .HasForeignKey("PropertyKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyKey");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Shop", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Market", null)
                        .WithMany("Shops")
                        .HasForeignKey("MarketId");

                    b.HasOne("MMABackend.DomainModels.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ShopLocationDetail", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.Market", "Market")
                        .WithMany()
                        .HasForeignKey("MarketId");

                    b.HasOne("MMABackend.DomainModels.Common.Shop", "Shop")
                        .WithOne("ShopLocationDetail")
                        .HasForeignKey("MMABackend.DomainModels.Common.ShopLocationDetail", "ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Market");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ShopPoint", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.ShopLocationDetail", "ShopLocationDetail")
                        .WithMany("ShopPoints")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShopLocationDetail");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.UserAvatar", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMABackend.DomainModels.Common.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MMABackend.DomainModels.Common.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.AuctionProduct", b =>
                {
                    b.Navigation("AuctionProductsUsers");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Category", b =>
                {
                    b.Navigation("CategoryPropertyKeys");

                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.CollectiveSoldProduct", b =>
                {
                    b.Navigation("CollectivePurchasers");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Market", b =>
                {
                    b.Navigation("Shops");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Product", b =>
                {
                    b.Navigation("AuctionProducts");

                    b.Navigation("CollectiveSoldProducts");

                    b.Navigation("Favorites");

                    b.Navigation("Photos");

                    b.Navigation("ProductProperties");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.PropertyKey", b =>
                {
                    b.Navigation("PropertyValues");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.PropertyValue", b =>
                {
                    b.Navigation("ProductProperties");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.Shop", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("ShopLocationDetail");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.ShopLocationDetail", b =>
                {
                    b.Navigation("ShopPoints");
                });

            modelBuilder.Entity("MMABackend.DomainModels.Common.User", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
