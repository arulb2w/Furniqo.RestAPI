namespace Furniqo.Product.API.Services
{
    public interface IMasterDataClient
    {
        Task<(string? CategoryName, string? SubCategoryName)> GetCategoryInfoAsync(Guid subCategoryId);
    }
}
