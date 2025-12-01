using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration_Group_8.Models;

namespace ProductRegistration_Group_8.Controllers;

public class CategoryController : Controller
{
    private readonly ProductContext _context;

    public CategoryController(ProductContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
                                       .Include(c => c.Products) 
                                       .ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _context.Categories
                                     .Include(c => c.Products)
                                     .FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null) return NotFound();
        return View(category);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (!ModelState.IsValid) return View(category);
        if (id != category.CategoryId) return BadRequest();

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Patch(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Patch(int id, string name)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        category.Name = name;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null) return NotFound();

        if (category.Products.Any())
        {
            ModelState.AddModelError("", "Cannot delete category because it has associated products.");
            return View(category);
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
