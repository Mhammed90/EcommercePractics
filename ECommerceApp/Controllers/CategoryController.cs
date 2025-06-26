using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public CategoryController(IUnitOfWork uoW) => _UoW = uoW;

    [HttpGet("categories")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _UoW.CategoryRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _UoW.CategoryRepository.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var newCategory = new Category { Name = category.Name };
        await _UoW.CategoryRepository.AddAsync(newCategory);
        await _UoW.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(Category category, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var categoryToUpdate = await _UoW.CategoryRepository.GetByIdAsync(id);
        if (categoryToUpdate == null)
            return NotFound();
        categoryToUpdate.Name = category.Name;
        _UoW.CategoryRepository.Update(categoryToUpdate);
        await _UoW.CommitAsync();
        return NoContent();
    }


    [HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _UoW.CategoryRepository.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        _UoW.CategoryRepository.Delete(category);
        await _UoW.CommitAsync();
        return NoContent();
    }


    [HttpGet("FindByName")]
    public async Task<IActionResult> Find(string name)
    {
        var res = await _UoW.CategoryRepository.FindAsync(nm => nm.Name == name);
        if (res == null)
            return NotFound();
        return Ok(res);
    }
}