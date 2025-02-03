using lab2.Orders.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Orders.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> Orders = [];

    [HttpGet]
    public IActionResult GetAll() => Ok(Orders);

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var order = Orders.FirstOrDefault(x => x.Id == id);
        return order is not null ? Ok(order) : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] Order newOrder)
    {
        newOrder.Id = Orders.Count > 0 ? Orders.Max(x => x.Id) + 1 : 1;
        Orders.Add(newOrder);
        return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Order updatedOrder)
    {
        var order = Orders.FirstOrDefault(x => x.Id == id);
        if (order is null) return NotFound();

        order.ProductName = updatedOrder.ProductName;
        order.Quantity = updatedOrder.Quantity;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var order = Orders.FirstOrDefault(x => x.Id == id);
        if (order is null) return NotFound();

        Orders.Remove(order);
        return NoContent();
    }
}