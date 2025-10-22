

using Domain.Entites.ProductModule;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey>where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>> Criteria { get; }
        public List<Expression<Func<TEntity,Object>>> IncludeExpressions { get; }


    }
}
