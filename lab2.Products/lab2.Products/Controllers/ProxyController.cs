using lab2.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Products.Controllers;

[ApiController]
[Route("api/proxy/orders")]
public class ProxyController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("OrdersApi");

    [HttpGet("")]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _httpClient.GetAsync("orders");
        response.EnsureSuccessStatusCode();
        var orders = await response.Content.ReadAsStringAsync();
        return Content(orders, "application/json");
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateOrder([FromBody] Order newOrder)
    {
        var response = await _httpClient.PostAsJsonAsync("orders", newOrder);
        response.EnsureSuccessStatusCode();
        var createdOrder = await response.Content.ReadAsStringAsync();
        return Content(createdOrder, "application/json");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrder)
    {
        var response = await _httpClient.PutAsJsonAsync($"orders/{id}", updatedOrder);
        return StatusCode((int)response.StatusCode);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var response = await _httpClient.DeleteAsync($"orders/{id}");
        return StatusCode((int)response.StatusCode);
    }
}