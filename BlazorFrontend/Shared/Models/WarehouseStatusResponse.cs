namespace BlazorFrontend.Shared.Models
{
    public class WarehouseStatusResponse
    {
        public DateTime LastChanged { get; set; }
        public List<Item> Items { get; set; } = new();
    }
}
