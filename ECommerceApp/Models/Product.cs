namespace ECommerceApp.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(250), MinLength(3)]
    public string Name { get; set; } = String.Empty;


    public decimal Price { get; set; }

    [Required, MaxLength(250), MinLength(5)]
    public string ImageUrl { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}