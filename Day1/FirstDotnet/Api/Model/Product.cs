namespace Api.Model;

public record Product
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
}

public interface IEntity
{
    int Id { get; }
    string Name { get; }
}
public record ProductAsRecord(int Id, string Name, decimal Price, string? Description) : IEntity;


class MyClass
{

    public MyClass()
    {
        Nullable<int> y = null;
        int? z = null;

        List<ProductAsRecord> products = new List<ProductAsRecord>
        {
            new ProductAsRecord(1, "Product1", 10.0m, "Description1"),
            new ProductAsRecord(2, "Product2", 20.0m, "Description2"),
            new ProductAsRecord(3, "Product3", 30.0m, null)
        };

        foreach (var item in products)
        {
            PrintProductName(item);
        }
        Console.WriteLine(  "____________________________");
        foreach (var item in products)
        {
            PrintProductName(item);
        }
    }

    Product? PrintProductName(Product? p)
    {
        var p1 = p;
        if (p1 is not null)
        {
            Console.WriteLine(p.Name);
            Console.WriteLine( p.Price);
            p1 = p with { Price = 0 };
        }
        return p1;
    }


    bool IsProductNotNull(Product? p)
    {
    
        
        return p is not null;
    }



}
