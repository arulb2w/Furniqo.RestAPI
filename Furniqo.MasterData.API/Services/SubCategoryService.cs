using Furniqo.MasterData.API.Configurations;
using Furniqo.MasterData.API.DTOs;
using Furniqo.MasterData.API.Models;
using Furniqo.MasterData.API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Furniqo.MasterData.API.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly AppDbContext _context;

    public SubCategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SubCategoryListDto>> GetAllAsync()
    {
        return await _context.SubCategories
            .Include(s => s.Category)
            .Select(s => new SubCategoryListDto
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                CategoryName = s.Category != null ? s.Category.Name : "",
                IsActive = s.IsActive
            }).ToListAsync();
    }

    public async Task<SubCategoryListDto?> GetByIdAsync(Guid id)
    {
        var sub = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null) return null;

        return new SubCategoryListDto
        {
            Id = sub.Id,
            Name = sub.Name,
            CategoryId = sub.CategoryId,
            CategoryName = sub.Category?.Name ?? "",
            IsActive = sub.IsActive
        };
    }

    public async Task<SubCategoryDto?> GetByIdAsyncWithCat(Guid id)
    {
        var sub = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null) return null;

        return new SubCategoryDto
        {
            Id = sub.Id,
            Name = sub.Name,
            CategoryName = sub.Category?.Name ?? "",
        };
    }

    public async Task<SubCategoryListDto> CreateAsync(SubCategoryCreateDto dto)
    {
        var model = new SubCategory
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        _context.SubCategories.Add(model);
        await _context.SaveChangesAsync();

        var category = await _context.Categories.FindAsync(dto.CategoryId);

        return new SubCategoryListDto
        {
            Id = model.Id,
            Name = model.Name,
            CategoryId = model.CategoryId,
            CategoryName = category?.Name ?? "",
            IsActive = model.IsActive
        };
    }

    public async Task<bool> UpdateAsync(SubCategoryUpdateDto dto)
    {
        var model = await _context.SubCategories.FindAsync(dto.Id);
        if (model == null) return false;

        model.Name = dto.Name;
        model.CategoryId = dto.CategoryId;
        model.IsActive = dto.IsActive;

        _context.SubCategories.Update(model);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await _context.SubCategories.FindAsync(id);
        if (model == null) return false;

        _context.SubCategories.Remove(model);
        await _context.SaveChangesAsync();

        return true;
    }
}
