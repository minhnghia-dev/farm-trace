namespace AgriTrace.API.Models
{
    public class ProductDto
    {
        // Thêm 'string.Empty' để xóa các cảnh báo CS8618 (Non-nullable property)
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Farm { get; set; } = string.Empty;
        public long HarvestDate { get; set; }
        public string Transporter { get; set; } = string.Empty;
    }
}