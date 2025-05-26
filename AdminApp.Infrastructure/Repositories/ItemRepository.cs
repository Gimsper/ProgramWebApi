using AdminApp.Core.Context;
using AdminApp.Core.Entities;
using AdminApp.Infrastructure.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Infrastructure.Repositories
{
    public class ItemRepository : _BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DBContext context) : base(context)
        {
        }

        public override async Task<List<Item>> GetAllAsync()
        {
            return await _context.Item.ToListAsync();
        }

        public override async Task<Item> GetByAsync(Expression<Func<Item, bool>> func)
        {
            return await _context.Item.FirstOrDefaultAsync(func);
        }

        public override async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Item.FirstOrDefaultAsync(i => i.ItemId == id);
        }

        public override async Task<Item> AddAsync(Item entity)
        {
            await _context.Item.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task UpdateAsync(Item entity)
        {
            _context.Item.Update(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
