using Domain.Contracts;
using Presistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;
        private ConcurrentDictionary<string, object> repositories;
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            repositories=new ConcurrentDictionary<string, object>();
            _storeDbContext = storeDbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var key =typeof(TEntity).Name;
            return (IGenericRepository<TEntity, TKey>)
                repositories.GetOrAdd(key,(_)=>new GenericRepository<TEntity,TKey>(_storeDbContext)
                );
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _storeDbContext.SaveChangesAsync();
        }
    }
}
