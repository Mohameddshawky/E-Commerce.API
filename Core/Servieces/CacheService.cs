using Domain.Contracts;
using Servces.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepository repository) : ICacheService
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
           var data=await  repository.GetAsync(key);
            return data==null? null :data;                                                                              
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
            await repository.SetAsync(key, value, duration);
        }
    }
}
