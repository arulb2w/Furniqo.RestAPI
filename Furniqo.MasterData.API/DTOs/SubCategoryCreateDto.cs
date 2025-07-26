namespace Furniqo.MasterData.API.DTOs;

public class SubCategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
public class SubCategoryUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public bool IsActive { get; set; }
}
public class SubCategoryListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
public class SubCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}
