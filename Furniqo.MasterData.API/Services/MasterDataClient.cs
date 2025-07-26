using Furniqo.MasterData.API.Services.Interface;
using System.Net.Http;
using System.Text.Json;

namespace Furniqo.Product.API.Services
{
    public class MasterDataClient : IMasterDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MasterDataClient> _logger;

        public MasterDataClient(HttpClient httpClient, ILogger<MasterDataClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(string? CategoryName, string? SubCategoryName)> GetCategoryInfoAsync(Guid subCategoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7014/api/master/subcategory-details/{subCategoryId}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch subcategory details from MasterData");
                    return (null, null);
                }

                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                var categoryName = doc.RootElement.GetProperty("categoryName").GetString();
                var subCategoryName = doc.RootElement.GetProperty("subCategoryName").GetString();

                return (categoryName, subCategoryName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling MasterDataService");
                return (null, null);
            }
        }
    }
}
