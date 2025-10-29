using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var data= await _database.StringGetAsync(key);
            return data.IsNullOrEmpty ? default : data;                                         
        }

        public async Task SetAsync(string key, object value, TimeSpan? Duration = null)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), Duration);
        }
    }
}
