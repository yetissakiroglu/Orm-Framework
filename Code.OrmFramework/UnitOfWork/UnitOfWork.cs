using AutoMapper;
using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using Code.OrmFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Code.OrmFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;
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

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
