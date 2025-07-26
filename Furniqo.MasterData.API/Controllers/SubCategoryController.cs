using Furniqo.MasterData.API.DTOs;
using Furniqo.MasterData.API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furniqo.MasterData.API.Controllers;

[ApiController]
[Route("api/masterdata/[controller]")]
[Authorize]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _service;

    public SubCategoryController(ISubCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }
    [HttpGet("getinfo/{id}")]
    public async Task<IActionResult> GetByIdAsyncWithCat(Guid id)
    {
        var item = await _service.GetByIdAsyncWithCat(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubCategoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SubCategoryUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
