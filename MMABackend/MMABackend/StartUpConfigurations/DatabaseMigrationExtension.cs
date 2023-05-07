using System;
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
            var created = uow.Database.EnsureCreated();
            if (created)
            {
                serviceProvider.InitializeUsersAndRoles();
                serviceProvider.CommonSeeding();
            }
      
        }
    }
}