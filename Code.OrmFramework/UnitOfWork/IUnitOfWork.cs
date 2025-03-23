using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using Code.OrmFramework.Repositories;

namespace Code.OrmFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TDto, TId> Repository<TEntity, TDto, TId>()
            where TEntity :  IEntity<TId>
            where TDto : class, IDto;

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
