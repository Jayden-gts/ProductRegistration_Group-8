using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration_Group_8.Models;

[Route("api/product")]
[ApiController]
public class ProductApiController : ControllerBase
{
    private readonly ProductContext _context;

    public ProductApiController(ProductContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts() => Ok(await _context.Products.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.ProductId) return BadRequest();
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduct(int id, [FromBody] double newPrice)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.Price = newPrice;
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
