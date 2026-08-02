using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }  // include

        Expression<Func<TEntity , bool>>? Cretirea { get; }  // where
        Expression<Func<TEntity, object>>? OrderBy { get; }  // Sort
        Expression<Func<TEntity, object>>? OrderByDesc { get; }  // Sort Desc 

        int Take { get; }
        int Skip { get; }

        bool IsPaginated { get; }

    }
}
