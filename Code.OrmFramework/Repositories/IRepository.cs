using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using System.Linq.Expressions;
using X.PagedList;

namespace Code.OrmFramework.Repositories
{
    public interface IRepository<TEntity, TDto, TId>
      where TEntity : IEntity<TId>
      where TDto : class, IDto
    {
        Task<TDto?> GetByIdAsync(TId id);
        Task<TDto?> GetForReadAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TDto?> GetForEditAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TDto>> WhereForReadAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TDto>> WhereForEditAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes);
        PagedList<TDto> PagedListForRead(Expression<Func<TEntity, bool>>? filters = null, int page = 1, int pageSize = 10);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filters = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task SoftDeleteAsync(TId id);
        Task SoftDeleteRangeAsync(IEnumerable<TId> ids);
        Task RestoreAsync(TId id);
        Task RestoreRangeAsync(IEnumerable<TId> ids);
    }
}
