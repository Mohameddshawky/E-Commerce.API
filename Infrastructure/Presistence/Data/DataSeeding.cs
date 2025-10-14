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
        
        public void SeedData()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }
                if (!_storeDbContext.productBrands.Any())
                {
                    var data = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var productBrand = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    if (productBrand != null && productBrand.Any())
                    {
                        _storeDbContext.productBrands.AddRange(productBrand);
                    }
                }
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var data = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var productType = JsonSerializer.Deserialize<List<ProductType>>(data);
                    if (productType != null && productType.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(productType);
                    }
                }
                _storeDbContext.SaveChanges();
                if (!_storeDbContext.products.Any())
                {
                    var data = File.ReadAllText("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(data);
                    if (product != null && product.Any())
                    {
                        _storeDbContext.products.AddRange(product);
                    }

                }
                _storeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                //handle ex
            }                         
        }
    }
}
