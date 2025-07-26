namespace Furniqo.Product.API.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public string SKU { get; set; }
}

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public string SKU { get; set; }
    public bool IsActive { get; set; }
}
public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? CategoryName { get; set; }
    public string? SubCategoryName { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public string SKU { get; set; }
    public bool IsActive { get; set; }
}