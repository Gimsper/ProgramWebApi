﻿using Microsoft.AspNetCore.Http;

namespace AdminApp.Core.DTO.Item
{
    public class ItemAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ImageType { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
