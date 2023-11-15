using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Organik.Case.Application.Interfaces;
using Organik.Case.Domain.Common;

namespace Organik.Case.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _userService;
        public IQueryable<TEntity> Table { get; } = null!;

        public GenericRepository(ApplicationDbContext context, ICurrentUserService userService)
        {
            _context = context;
            _userService = userService;
            Table = context.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var result = Table.AsQueryable();
            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            return await result.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(uint id)
        {
            var result = Table.AsQueryable().AsNoTracking();
            return await result.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var result = Table.AsQueryable();
            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            return await result.FirstOrDefaultAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Add(entity);
            if (entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.CreatedBy = _userService.UserId;
                auditableEntity.Created = DateTime.Now;
                auditableEntity.LastModifiedBy = null;
                auditableEntity.LastModified = null;
            }
            await SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            if (entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.LastModifiedBy = _userService.UserId;
                auditableEntity.LastModified = DateTime.Now;
                _context.Entry(auditableEntity).Property(x => x.Created).IsModified = false;
                _context.Entry(auditableEntity).Property(x => x.CreatedBy).IsModified = false;
            }
            await SaveChangesAsync();
        }

        public async Task<TEntity> UpsertAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id > 0)
            {
                await UpdateAsync(entity);
                return entity;
            }
            await InsertAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            await SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}

