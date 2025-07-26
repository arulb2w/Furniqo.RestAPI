namespace Furniqo.MasterData.API.Services.Interface
{
    public interface IMasterDataClient
    {
        Task<(string? CategoryName, string? SubCategoryName)> GetCategoryInfoAsync(Guid subCategoryId);
    }
}
