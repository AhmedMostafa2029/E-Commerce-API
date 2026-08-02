using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Where
        public Expression<Func<TEntity, bool>> Cretirea { get; private set; }
        public BaseSpecifications(Expression<Func<TEntity, bool>> Cretirea = null)
        {
            this.Cretirea = Cretirea;
        }
        #endregion

        #region Include

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];


        public void AddInclude(Expression<Func<TEntity , object>> expression)
        {
            IncludeExpressions.Add(expression);
        }
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>>? OrderBy { get; private set;  }

        public void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
            => OrderBy = orderBy;


        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }


        public void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDesc)
            => OrderByDesc = orderByDesc;

        #endregion


        #region Pagination

        public int Take {  get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        public void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        #endregion


    }
}
