using AdminApp.Core.Context;
using AdminApp.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace AdminApp.Infrastructure.Repositories
{
    public abstract class _BaseRepository<T> : _IBaseRepository<T> where T : class
    {
        protected readonly DBContext _context;

        public _BaseRepository(DBContext context)
        {
            _context = context;
        }

        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T> GetByAsync(Expression<Func<T, bool>> func);
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task<T> AddAsync(T entity);
        public abstract Task UpdateAsync(T entity);
        public abstract Task DeleteAsync(int id);
    }
}
