using Furniqo.Product.API.DTOs;
using Furniqo.Product.API.Services;
using Furniqo.Product.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Furniqo.Product.API.Controllers
{
    [ApiController]
    [Route("api/product/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
       /* public async Task<IEnumerable<ProductListDto>> GetAllAsync()
        {
            var products = await _service.GetAll();

            // 1. Extract unique SubCategoryIds
            var subCategoryIds = products.Select(p => p.SubCategoryId).Distinct().ToList();

            // 2. Call MasterDataService to get subcategory + category names
            var subCategoryDetails = await _masterDataClient.GetSubCategoryDetails(subCategoryIds);

            // 3. Join and map
            return products.Select(p =>
            {
                var subCat = subCategoryDetails.FirstOrDefault(s => s.Id == p.SubCategoryId);

                return new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    SKU = p.SKU,
                    Price = p.Price,
                    StockQty = p.StockQty,
                    IsActive = p.IsActive,
                    CategoryName = subCat?.CategoryName,
                    SubCategoryName = subCat?.Name
                };
            });
        }*/

        public async Task<IActionResult> GetAll()
         {
             var products = await _service.GetAllAsync();
             return Ok(products);
         }

        [HttpGet("GetByIdAsyncWithCat/{id}")]
        public async Task<IActionResult> GetByIdAsyncWithCat(Guid id)
        {
            var productDetails = await _service.GetByIdAsyncWithCat(id);
            if (productDetails == null)
                return NotFound();

            return Ok(productDetails);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto dto)
        {
            var success = await _service.UpdateAsync(dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
