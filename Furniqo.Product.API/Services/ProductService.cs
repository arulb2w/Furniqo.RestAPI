using Furniqo.Product.API.Configurations;
using Furniqo.Product.API.DTOs;
using Furniqo.Product.API.Models;
using Furniqo.Product.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Furniqo.Product.API.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMasterDataClient _masterDataClient;
        public ProductService(AppDbContext context, IMasterDataClient masterDataClient)
        {
            _context = context;
            _masterDataClient = masterDataClient;
        }
        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            return await _context.ProductEntity.Where(p => p.IsActive == true).ToListAsync();
        }
        public async Task<IEnumerable<ProductListDto>> GetAllAsync()
        {
            return await _context.ProductEntity
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    SKU = p.SKU,
                    StockQty = p.StockQty,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }
        public async Task<ProductListDto?> GetByIdAsyncWithCat(Guid id)
        {
            var product = await _context.ProductEntity.FindAsync(id);
            if (product == null) return null;

            var (categoryName, subCategoryName) = await _masterDataClient.GetCategoryInfoAsync(product.SubCategoryId);

            return new ProductListDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                SKU = product.SKU,
                StockQty = product.StockQty,
                CategoryName = categoryName,
                SubCategoryName = subCategoryName
            };
        }

        public async Task<ProductListDto?> GetByIdAsync(Guid id)
        {
            var p = await _context.ProductEntity.FindAsync(id);
            if (p == null) return null;

            return new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SKU = p.SKU,
                StockQty = p.StockQty,
                IsActive = p.IsActive
            };
        }

        public async Task<ProductListDto> CreateAsync(ProductCreateDto dto)
        {
            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                SKU = dto.SKU,
                StockQty = dto.StockQty,
                SubCategoryId = dto.SubCategoryId,
                CreatedDate = DateTime.UtcNow
            };

            _context.ProductEntity.Add(product);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(product.Id) ?? throw new Exception("Failed to create product");
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto dto)
        {
            var product = await _context.ProductEntity.FindAsync(dto.Id);
            if (product == null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.SKU = dto.SKU;
            product.StockQty = dto.StockQty;
            product.IsActive = dto.IsActive;
            product.SubCategoryId = dto.SubCategoryId;

            _context.ProductEntity.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.ProductEntity.FindAsync(id);
            if (product == null) return false;

            _context.ProductEntity.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
