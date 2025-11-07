using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Extension
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CrosPolicy", builder =>
                {
                    builder.WithOrigins(configuration.GetSection("URLS")["frontendURL"])
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
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
