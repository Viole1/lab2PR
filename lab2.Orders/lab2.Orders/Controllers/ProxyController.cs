using lab2.Orders.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Orders.Controllers;

[ApiController]
[Route("api/proxy/products")]
public class ProxyController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ProductsApi");

    [HttpGet("")]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _httpClient.GetAsync("products");
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadAsStringAsync();
        return Content(products, "application/json");
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
    {
        var response = await _httpClient.PostAsJsonAsync("products", newProduct);
        response.EnsureSuccessStatusCode();
        var createdProduct = await response.Content.ReadAsStringAsync();
        return Content(createdProduct, "application/json");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var response = await _httpClient.PutAsJsonAsync($"products/{id}", updatedProduct);
        return StatusCode((int)response.StatusCode);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await _httpClient.DeleteAsync($"products/{id}");
        return StatusCode((int)response.StatusCode);
    }
}