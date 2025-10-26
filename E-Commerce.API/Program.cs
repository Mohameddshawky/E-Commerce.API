
using Domain.Contracts;
using E_Commerce.API.Factories;
using E_Commerce.API.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repositories;
using Servces.Abstraction;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            }
                );
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;

            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>(); 
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(c =>{ },typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var obj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await obj.SeedDataAsync();
            }
            // Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();   
            // app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
