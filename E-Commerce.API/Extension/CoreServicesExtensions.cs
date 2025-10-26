using Servces.Abstraction;
using Services;
using Servieces;


namespace E_Commerce.API.Extention
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)//the this keyword makes it an extension method for the IServiceCollection type. 
        {
            services.AddAutoMapper(c => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();

            return services;                                        

        }
    }
}

