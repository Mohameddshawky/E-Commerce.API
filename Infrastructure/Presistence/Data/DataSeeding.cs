using Azure.Core.Serialization;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _storeDbContext) : IDataSeeding
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
               await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //handle ex
            }                         
        }
    }
}
