﻿namespace AdminApp.Core.DTO.Item
{
    public class ItemReadDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ImageType { get; set; } = string.Empty;
    }
}
