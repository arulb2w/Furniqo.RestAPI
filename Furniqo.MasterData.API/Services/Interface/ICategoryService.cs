using Furniqo.MasterData.API.DTOs;

namespace Furniqo.MasterData.API.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListDto>> GetAllAsync();
        Task<CategoryListDto?> GetByIdAsync(Guid id);
        Task<CategoryListDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(CategoryUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
