using Furniqo.MasterData.API.Configurations;
using Furniqo.MasterData.API.DTOs;
using Furniqo.MasterData.API.Models;
using Furniqo.MasterData.API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Furniqo.MasterData.API.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryListDto>> GetAllAsync()
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryListDto?> GetByIdAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null || !category.IsActive) return null;

        return new CategoryListDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryListDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            IsActive = true
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryListDto { Id = category.Id, Name = category.Name };
    }

    public async Task<bool> UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _context.Categories.FindAsync(dto.Id);
        if (category == null) return false;

        category.Name = dto.Name;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        category.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}
