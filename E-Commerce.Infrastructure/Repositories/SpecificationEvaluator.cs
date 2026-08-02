using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> createQuery<TEntity , TKey>(IQueryable<TEntity> InputQuery ,ISpecifications<TEntity, TKey> specifications)
            where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;

            if(specifications.IncludeExpressions.Count > 0)
            {
                // DbContext.Set<T>().Include(brand).Include(type)
                query = specifications.IncludeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            }

            if(specifications.Cretirea is not null)
            {
                query = query.Where(specifications.Cretirea);
            }
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            if(specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return query;
        }

    }
}
