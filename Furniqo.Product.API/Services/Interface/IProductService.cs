using Furniqo.Product.API.DTOs;
using Furniqo.Product.API.Models;

namespace Furniqo.Product.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<IEnumerable<ProductListDto>> GetAllAsync();
        Task<ProductListDto?> GetByIdAsyncWithCat(Guid id);
        Task<ProductListDto?> GetByIdAsync(Guid id);
        Task<ProductListDto> CreateAsync(ProductCreateDto dto);
        Task<bool> UpdateAsync(ProductUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
