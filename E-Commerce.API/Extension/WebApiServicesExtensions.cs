using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Extension
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;

            });
            return services;
        } 
    }
}
