using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MMABackend.DataAccessLayer;

namespace MMABackend.StartUpConfigurations
{
    public static class DatabaseMigrationExtension
    {
        public static void DataSeed(this IServiceProvider serviceProvider)
        {
            var uow = serviceProvider?.GetService<UnitOfWork>()
                          ?? throw new ArgumentNullException(nameof(serviceProvider));
            // uow.Database.EnsureDeleted();
            Directory.CreateDirectory("database");
            var created = uow.Database.EnsureCreated();
            if (created)
            {
                serviceProvider.InitializeUsersAndRoles();
                serviceProvider.CommonSeeding();
            }
      
        }
    }
}