using AdminApp.Core.Entities;
using AdminApp.Infrastructure.Interfaces;
using AdminApp.Services.Interfaces;

namespace AdminApp.Services.Services
{
    public class ItemService : _BaseService<Item>, IItemService
    {
        public ItemService(IItemRepository repository) : base(repository)
        {
        }
    }
}
