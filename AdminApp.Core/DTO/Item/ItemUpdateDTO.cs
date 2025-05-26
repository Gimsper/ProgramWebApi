namespace AdminApp.Core.DTO.Item
{
    public class ItemUpdateDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
