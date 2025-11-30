using Microsoft.AspNetCore.Mvc;
using ProductRegistration_Group_8.Models;
using System.Net.Http.Json;

namespace ProductRegistration_Group_8.Controllers;

public class ProductController : Controller
{
    private readonly HttpClient _client;

    public ProductController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        //var products = await _client.GetFromJsonAsync<List<Product>>("Product");
        //return View(products);
        return View(new List<Product>());

    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        if (product == null) return NotFound();
        return View(product);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);

        var response = await _client.PostAsJsonAsync("Product", product);
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to create product");
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (!ModelState.IsValid) return View(product);

        var response = await _client.PutAsJsonAsync($"Product/{id}", product);
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to update product");
        return View(product);
    }

    public async Task<IActionResult> Patch(int id)
    {
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Patch(int id, double price)
    {
        var patchObj = new { Price = price };
        var response = await _client.PatchAsJsonAsync($"Product/{id}", patchObj);

        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to patch product");
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _client.DeleteAsync($"Product/{id}");
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");

        ModelState.AddModelError("", "Failed to delete product");
        var product = await _client.GetFromJsonAsync<Product>($"Product/{id}");
        return View(product);
    }
}
