using Services.Abstraction;
using Services;
using Servieces;
using Shared.Common;


namespace E_Commerce.API.Extention
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)//the this keyword makes it an extension method for the IServiceCollection type. 
        {
            services.AddAutoMapper(c => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;                                        

        }
    }
}

