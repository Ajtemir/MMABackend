﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(IServiceScopeFactory ssf)
        {
            _serviceScopeFactory = ssf;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var uow = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<UnitOfWork>();
                uow.CollectiveSoldProducts.Where(x => x.EndDate <= DateTime.Now).ToList().ForEach(x =>
                {
                    x.Status = CollectiveProductStatus.Expired;
                });
                await uow.SaveChangesAsync();
            }
        }
        
    }
}