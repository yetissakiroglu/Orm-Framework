using AutoMapper;
using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using Code.OrmFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Code.OrmFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IRepository<TEntity, TDto> Repository<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class, IDto
        {
            return new Repository<TEntity, TDto>(_context, _mapper);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
