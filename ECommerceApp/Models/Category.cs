using ECommerceApp.Models;

public class Category
{
    public int Id { get; set; }

    [Required, MaxLength(50), MinLength(3)]
    public string Name { get; set; } = String.Empty;

    public HashSet<Product> Products { get; set; } = new HashSet<Product>();
}