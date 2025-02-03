namespace lab2.Products.Models;

public class Order
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
}