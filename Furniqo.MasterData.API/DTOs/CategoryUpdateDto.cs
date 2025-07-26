namespace Furniqo.MasterData.API.DTOs;

public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class CategoryListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
}