using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;
using StackExchange.Redis;

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

            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>
                ((_) =>
                {
                   return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
                });

            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;

            
        }
    }
}
