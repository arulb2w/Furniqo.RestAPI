namespace Furniqo.Product.API.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string SKU { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
