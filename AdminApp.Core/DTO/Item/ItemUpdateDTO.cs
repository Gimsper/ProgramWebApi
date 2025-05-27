using Microsoft.AspNetCore.Http;

namespace AdminApp.Core.DTO.Item
{
    public class ItemUpdateDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
