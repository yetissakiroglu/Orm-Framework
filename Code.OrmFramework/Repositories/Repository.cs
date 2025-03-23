using AutoMapper;
using Code.OrmFramework.DTOs;
using Code.OrmFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
using X.PagedList.Extensions;

namespace Code.OrmFramework.Repositories
{
    public class Repository<TEntity, TDto, TId> : IRepository<TEntity, TDto, TId>
             where TEntity :  IEntity<TId>
             where TDto : class, IDto
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;

        public Repository(DbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TDto?> GetByIdAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null ? _mapper.Map<TDto>(entity) : null;
        }

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _dbSet.AddRangeAsync(entities);
        public Task UpdateAsync(TEntity entity) { _dbSet.Update(entity); return Task.CompletedTask; }
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities) { _dbSet.UpdateRange(entities); return Task.CompletedTask; }
        public Task DeleteAsync(TEntity entity) { _dbSet.Remove(entity); return Task.CompletedTask; }
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities) { _dbSet.RemoveRange(entities); return Task.CompletedTask; }

        public async Task<TDto?> GetForReadAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (filters != null)
                query = query.Where(filters);
            foreach (var include in includes)
                query = query.Include(include);
            var entity = await query.FirstOrDefaultAsync();
            return entity != null ? _mapper.Map<TDto>(entity) : null;
        }

        public async Task<TDto?> GetForEditAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetForReadAsync(filters, includes);
        }

        public async Task<List<TDto>> WhereForReadAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (filters != null)
                query = query.Where(filters);
            foreach (var include in includes)
                query = query.Include(include);
            var entities = await query.ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<List<TDto>> WhereForEditAsync(Expression<Func<TEntity, bool>>? filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return await WhereForReadAsync(filters, includes);
        }

        //public async Task<PagedList<TDto>> PagedListForReadAsync(Expression<Func<TEntity, bool>>? filters = null, int page = 1, int pageSize = 10)
        //{
        //    var query = _dbSet.AsQueryable();
        //    if (filters != null)
        //        query = query.Where(filters);
        //    var pagedList = await query.ToPagedListAsync(page, pageSize);
        //    return _mapper.Map<PagedList<TDto>>(pagedList);
        //}

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filters = null)
        {
            return filters != null ? await _dbSet.AnyAsync(filters) : await _dbSet.AnyAsync();
        }

        public async Task SoftDeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
        }

        public async Task SoftDeleteRangeAsync(IEnumerable<TId> ids)
        {
            var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task RestoreAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                _dbSet.Update(entity);
            }
        }

        public async Task RestoreRangeAsync(IEnumerable<TId> ids)
        {
            var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }
            _dbSet.UpdateRange(entities);
        }

        public PagedList<TDto> PagedListForRead(Expression<Func<TEntity, bool>>? filters = null, int page = 1, int pageSize = 10)
        {
            var query = _dbSet.AsQueryable();
            if (filters != null)
                query = query.Where(filters);
            var pagedList = query.ToPagedList(page, pageSize);
            return _mapper.Map<PagedList<TDto>>(pagedList);
        }
    }
}
