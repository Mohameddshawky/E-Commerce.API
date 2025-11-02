using Domain.Contracts;
using Domain.Entites.IdentitiyModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using Shared.Common;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce.API.Extension
{
    public static class InfraStructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefualtConnection"));
            });
            services.AddDbContext<IdentityStoreDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });


            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheRepository, CacheRepository>();    
            services.AddSingleton<IConnectionMultiplexer>
                ((_) =>
                {
                   return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
                });
            services.AddIdentity<User, IdentityRole>(op =>
            {
                op.Password.RequireDigit = true;    
                op.Password.RequireLowercase = true;
                op.Password.RequireUppercase = true;
                op.User.RequireUniqueEmail = true;  
            }).AddEntityFrameworkStores<IdentityStoreDbContext>()
                .AddDefaultTokenProviders();
            //services.AddIdentityCore<User>(op =>
            //{
            //    op.Password.RequireDigit = true;
            //    op.Password.RequireLowercase = true;
            //    op.Password.RequireUppercase = true;
            //    op.User.RequireUniqueEmail = true;
            //}).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityStoreDbContext>()
            //    .AddDefaultTokenProviders();    

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.ValidateJwt(configuration);
            return services;

            
        }
    }
}
