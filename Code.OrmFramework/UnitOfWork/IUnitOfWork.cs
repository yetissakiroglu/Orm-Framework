using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using Code.OrmFramework.Repositories;

namespace Code.OrmFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TDto> Repository<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class, IDto;

        Task SaveChangesAsync();
    }
}
