namespace Api.Model;

public class Product
{
    public int Id { get;}
    public  string Name { get;  }
    public decimal Price { get;  }
    public string? Description { get;  }

    public Product(int id, string name, decimal price, string? description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}

class MyClass
{

    public MyClass()
    {
        Nullable<int> y = null;
        int? z = null;

        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10.0m, Description = "Description1" },
            new Product { Id = 2, Name = "Product2", Price = 20.0m, Description = "Description2" },
            new Product { Id = 3, Name = "Product3", Price = 30.0m, Description = null }
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




















    void PrintProductName(Product? p)
    {
        if (p is not null)
        {
            Console.WriteLine(p.Name);
            Console.WriteLine( p.Price);
            p.Price = 0;

        }
    }


    bool IsProductNotNull(Product? p)
    {
    
        
        return p is not null;
    }



}
