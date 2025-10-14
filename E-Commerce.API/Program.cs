
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
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
            builder.Services.AddScoped<IDataSeeding,DataSeeding>(); 
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var obj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                obj.SeedData();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

           // app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
