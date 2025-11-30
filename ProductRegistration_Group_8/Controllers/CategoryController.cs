using Microsoft.AspNetCore.Mvc;
using ProductRegistration_Group_8.Models;
using System.Net.Http.Json;

namespace ProductRegistration_Group_8.Controllers;

public class CategoryController : Controller
{
    private readonly HttpClient _client;

    public CategoryController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        //var categories = await _client.GetFromJsonAsync<List<Category>>("Category");
        //return View(categories);
        return View(new List<Category>());

    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        if (category == null) return NotFound();
        return View(category);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);

        var response = await _client.PostAsJsonAsync("Category", category);
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to create category");
        return View(category);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (!ModelState.IsValid) return View(category);

        var response = await _client.PutAsJsonAsync($"Category/{id}", category);
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to update category");
        return View(category);
    }

    public async Task<IActionResult> Patch(int id)
    {
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Patch(int id, string name)
    {
        var patchObj = new { Name = name };
        var response = await _client.PatchAsJsonAsync($"Category/{id}", patchObj);

        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to patch category");
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        return View(category);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _client.DeleteAsync($"Category/{id}");
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to delete category");
        var category = await _client.GetFromJsonAsync<Category>($"Category/{id}");
        return View(category);
    }
}
