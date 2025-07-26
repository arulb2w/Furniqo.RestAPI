using Furniqo.MasterData.API.DTOs;

namespace Furniqo.MasterData.API.Services.Interface;

public interface ISubCategoryService
{
    Task<IEnumerable<SubCategoryListDto>> GetAllAsync();
    Task<SubCategoryListDto?> GetByIdAsync(Guid id);
    Task<SubCategoryDto?> GetByIdAsyncWithCat(Guid id);
    Task<SubCategoryListDto> CreateAsync(SubCategoryCreateDto dto);
    Task<bool> UpdateAsync(SubCategoryUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}