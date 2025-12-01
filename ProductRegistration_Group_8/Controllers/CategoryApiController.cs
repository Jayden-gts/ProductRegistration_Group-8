using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration_Group_8.Models;

[Route("api/category")]
[ApiController]
public class CategoryApiController : ControllerBase
{
    private readonly ProductContext _context;

    public CategoryApiController(ProductContext context)
    {
        _context = context;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Categories.ToListAsync());
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    // POST: api/Category
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, category);
    }

    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Category category)
    {
        if (id != category.CategoryId) return BadRequest();

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/Category/5 
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] string newName)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        category.Name = newName; 
        await _context.SaveChangesAsync();

        return Ok(category);
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
