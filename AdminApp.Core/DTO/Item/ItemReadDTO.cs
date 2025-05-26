namespace AdminApp.Core.DTO.Item
{
    public class ItemReadDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
