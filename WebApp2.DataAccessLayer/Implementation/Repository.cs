using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp2.DataAccessLayer.Interface;
using WebApp2.Model.Entity;
using WebApp2.Model.ResponseModel;


namespace WebApp2.DataAccessLayer.Implementation
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly NpgSqlDBContext _db;
        private DbSet<T> _dbSet;

        public Repository(NpgSqlDBContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(true);
            return entity;
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(true);
        }
        public async Task<IQueryable<T>> GetAllAsQueryable()
        {
            return await Task.FromResult(_dbSet.AsNoTracking());
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync(cancellationToken);
        }
        public Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public Task RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _dbSet.FindAsync(new object[] { id }, cancellationToken);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                return true;
            }

            return false;
        }

    }
}
