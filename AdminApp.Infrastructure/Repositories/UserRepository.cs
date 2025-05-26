using AdminApp.Core.Context;
using AdminApp.Core.Entities;
using AdminApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdminApp.Infrastructure.Repositories
{
    public class UserRepository : _BaseRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {
        }

        public override async Task<List<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public override async Task<User> GetByAsync(Expression<Func<User, bool>> func)
        {
            return await _context.User.FirstOrDefaultAsync(func);
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(i => i.UserId == id);
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _context.User.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task UpdateAsync(User entity)
        {
            _context.User.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.User.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
