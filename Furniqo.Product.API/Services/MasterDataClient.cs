using Furniqo.Product.API.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

public class MasterDataClient : IMasterDataClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MasterDataClient> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MasterDataClient(HttpClient httpClient, ILogger<MasterDataClient> logger, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(string?, string?)> GetCategoryInfoAsync(Guid subCategoryId)
    {
        try
        {
            // 🔐 Get token from current request context
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
            }

            var response = await _httpClient.GetAsync($"/api/masterdata/SubCategory/getinfo/{subCategoryId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch from MasterDataService: {response.StatusCode}");
                return (null, null);
            }

            var content = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(content);

            var categoryName = doc.RootElement.GetProperty("categoryName").GetString();
            var subCategoryName = doc.RootElement.GetProperty("name").GetString();

            return (categoryName, subCategoryName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling MasterDataService");
            return (null, null);
        }
    }
}
