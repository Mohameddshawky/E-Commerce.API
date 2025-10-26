using Domain.Contracts;
using E_Commerce.API.MiddleWares;

namespace E_Commerce.API.Extension
{
    public static class WebApplicationExtensions
    {
        public async static Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var obj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await obj.SeedDataAsync();
            }
            return app;
        }

        public static WebApplication AddExceptionHandlingMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;

        }

        public static WebApplication AddSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
