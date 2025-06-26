using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public ProductController(IUnitOfWork unitOfWork) => _UoW = unitOfWork;


    [HttpGet("{id:int}")]
    public async Task<IActionResult?> GetProduct(int id)
    {
        return Ok(await _UoW.ProductRepository.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var pro = new Product
        {
            Description = product.Description, Name = product.Name, Price = product.Price,
            CategoryId = product.CategoryId
        };
        await _UoW.ProductRepository.AddAsync(pro);
        await _UoW.CommitAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = pro.Id }, pro);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _UoW.ProductRepository.GetAllAsync());


    [HttpGet("search")]
    public async Task<IActionResult> GetAllByNAm(string name)
    {
        return Ok(await _UoW.ProductRepository.FindAsync(n => n.Name == name));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _UoW.ProductRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        _UoW.ProductRepository.Delete(product);
        await _UoW.CommitAsync();
        return NoContent();
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Update(Product product, int productId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var productToUpdate = await _UoW.ProductRepository.GetByIdAsync(productId);
        if (productToUpdate == null)
            return NotFound();
        productToUpdate.Description = product.Description;
        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.CategoryId = product.CategoryId;
        _UoW.ProductRepository.Update(productToUpdate);
        await _UoW.CommitAsync();
        return NoContent();
    }
}