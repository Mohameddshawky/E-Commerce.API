using Azure.Core.Serialization;
using Domain.Contracts;
using Domain.Entites.IdentitiyModule;
using Domain.Entites.OrderModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Data
{
    public class DataSeeding(
        StoreDbContext _storeDbContext,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
        ) : IDataSeeding
    {
        
        public async Task SeedDataAsync()
        {
            try
            {
                if ((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                   await _storeDbContext.Database.MigrateAsync();
                }
                if (!_storeDbContext.productBrands.Any())
                {
                    var data =  File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var productBrand =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(data);
                    if (productBrand != null && productBrand.Any())
                    {
                        await _storeDbContext.productBrands.AddRangeAsync(productBrand);
                    }
                }
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var data = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var productType =await JsonSerializer.DeserializeAsync<List<ProductType>>(data);
                    if (productType != null && productType.Any())
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(productType);
                    }
                }
                //_storeDbContext.SaveChanges();
                if (!_storeDbContext.products.Any())
                {
                    var data = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var product =await JsonSerializer.DeserializeAsync<List<Product>>(data);
                    if (product != null && product.Any())
                    {
                       await _storeDbContext.products.AddRangeAsync(product);
                    }

                }
                if (!_storeDbContext.DeliveryMethods.Any())
                {
                    var data = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\delivery.json");
                    var deliveryMethods =await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(data);
                    if (deliveryMethods != null && deliveryMethods.Any())
                    {
                       await _storeDbContext.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    }

                }
               await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //handle ex
            }                         
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    User Admin = new()
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01123456789"
                    };
                    User SuperAdmin = new()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01223456789"
                    };
                    await userManager.CreateAsync(Admin, "P@ssw0rd");
                    await userManager.CreateAsync(SuperAdmin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(Admin, "Admin");
                    await userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                //handle ex
            }   
        }
    }
}
