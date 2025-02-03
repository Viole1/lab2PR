using lab2.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Products.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = [];

    [HttpGet]
    public IActionResult GetAll() => Ok(Products);

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var product = Products.FirstOrDefault(x => x.Id == id);
        return product is not null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product newProduct)
    {
        newProduct.Id = Products.Count > 0 ? Products.Max(x => x.Id) + 1 : 1;
        Products.Add(newProduct);
        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Product updatedProduct)
    {
        var product = Products.FirstOrDefault(x => x.Id == id);
        if (product is null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(x => x.Id == id);
        if (product is null) return NotFound();

        Products.Remove(product);
        return NoContent();
    }
}