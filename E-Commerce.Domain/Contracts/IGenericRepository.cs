using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        void Add(TEntity entity);
        void Upadte(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct= default);

        Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken ct = default);


        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications,CancellationToken ct = default);

        Task<int> GetProductCountWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken ct = default);
    }
}
