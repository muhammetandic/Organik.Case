using System.Linq.Expressions;
using Organik.Case.Domain.Common;

namespace Organik.Case.Application.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        IQueryable<TEntity> Table { get; }

        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity?> GetByIdAsync(uint id);

        Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity> InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}

