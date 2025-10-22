using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(
            IQueryable<TEntity>InputQuery, ISpecifications<TEntity, TKey> specifications
            ) where TEntity :BaseEntity<TKey>
        {
            var query = InputQuery;

            if(specifications.Criteria is not null)
            {
                query=query.Where(specifications.Criteria);
            }
            if(specifications.IncludeExpressions is not null 
                && specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions
                    .Aggregate(query, (cur, ex) => cur.Include(ex));
            }
            return query;

        }
    }
}
