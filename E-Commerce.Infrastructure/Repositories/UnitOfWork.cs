using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext dbContext;
        private readonly Dictionary<string, object> _Repos = [];

        public UnitOfWork(StoreDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name; // String  Key
            if (_Repos.TryGetValue(TypeName, out object oldRepo))
            {
                return (IGenericRepository<TEntity , TKey>) oldRepo;
            }
            var newRepo = new GenericRepository<TEntity , TKey>(dbContext);
            _Repos[TypeName] = newRepo;

            return newRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
