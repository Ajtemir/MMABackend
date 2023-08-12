using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMABackend.Configurations.Users;
using MMABackend.CustomMiddlewares;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Managers.Users;
using MMABackend.Services.Users;
using MMABackend.StartUpConfigurations;

namespace MMABackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersManager, UsersManager>();
            services.AddMemoryCache();
            services.AddCustomJwtConfigurations();
            services.AddCors();
            services.AddDbContext<UnitOfWork>(options =>
                {
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                }
            );
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UnitOfWork>().AddDefaultTokenProviders();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerConfigurations();
            // services.AddScoped<ValidationFilterAttribute>();
            // services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UnitOfWork uow, IServiceProvider serviceProvider)
        {
            app.UseStaticFiles();
            // app.UseMiddleware<ExceptionHandlingMiddleware>();
            if (env.IsDevelopment() == env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MMABackend v1"));
            }
            serviceProvider.DataSeed();
            // app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}