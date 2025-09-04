namespace Api.Model;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}

class MyClass
{

    public MyClass()
    {
        Nullable<int> y = null;
        int? z = null;

        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = null, Price = 10.0m, Description = "Description1" },
            new Product { Id = 2, Name = "Product2", Price = 20.0m, Description = "Description2" },
            new Product { Id = 3, Name = "Product3", Price = 30.0m, Description = null }
        };

        var p = products.FirstOrDefault(p => p.Price > 50);
        if (IsProductNotNull(p))
        {
            Console.WriteLine(p!.Name);
        }
    }

    bool IsProductNotNull(Product? p)
    {
        return p is not null;
    }



}
