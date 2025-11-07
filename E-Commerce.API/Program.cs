
using Domain.Contracts;
using E_Commerce.API.Extension;
using E_Commerce.API.Extention;
using E_Commerce.API.Factories;
using E_Commerce.API.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repositories;
using Services.Abstraction;
using Services;
using Servieces;
using System.Threading.Tasks;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            #region DI Container

            builder.Services.AddWebApiServices(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            #endregion
            var app = builder.Build();

            await app.SeedDatabaseAsync();

            app.AddExceptionHandlingMiddleWare();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.AddSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CrosPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
