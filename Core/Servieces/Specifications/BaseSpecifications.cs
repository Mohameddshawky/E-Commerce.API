﻿using Domain.Contracts;
using Domain.Entites.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>

    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria= criteria; 
        }
        public Expression<Func<TEntity, bool>> Criteria{ get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }=new();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    }
}
