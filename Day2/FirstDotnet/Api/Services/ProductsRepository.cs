using Api.Contracts;
using Api.Model;

namespace Api.Services;

public class InMemoryProductsRepository : IProductsRepository
{
    public InMemoryProductsRepository()
    {
            
    }

    public Task<List<ProductEntity>> GetAllProducts()
    {
        var products = new List<ProductEntity>
        {
            new ProductEntity { Id = 1, Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", Modified = DateTime.Now.AddDays(-5), CategoryId = 1 },
            new ProductEntity { Id = 2, Name = "Smartphone", Price = 699.99m, Description = "Latest model smartphone", Modified = DateTime.Now.AddDays(-3), CategoryId = 1 },
            new ProductEntity { Id = 3, Name = "Coffee Mug", Price = 12.99m, Description = "Ceramic coffee mug", Modified = DateTime.Now.AddDays(-1), CategoryId = 2 },
            new ProductEntity { Id = 4, Name = "Desk Chair", Price = 249.99m, Description = "Ergonomic office chair", Modified = DateTime.Now.AddDays(-7), CategoryId = 3 },
            new ProductEntity { Id = 5, Name = "Headphones", Price = 149.99m, Description = "Wireless noise-canceling headphones", Modified = DateTime.Now.AddDays(-2), CategoryId = 1 },
            new ProductEntity { Id = 6, Name = "Book", Price = 19.99m, Description = "Programming guide", Modified = DateTime.Now.AddDays(-4), CategoryId = 4 },
            new ProductEntity { Id = 7, Name = "Monitor", Price = 299.99m, Description = "24-inch LED monitor", Modified = DateTime.Now.AddDays(-6), CategoryId = 1 },
            new ProductEntity { Id = 8, Name = "Pen Set", Price = 29.99m, Description = "Premium pen collection", Modified = DateTime.Now.AddDays(-8), CategoryId = 5 },
            new ProductEntity { Id = 9, Name = "Tablet", Price = 399.99m, Description = "10-inch tablet", Modified = DateTime.Now.AddDays(-9), CategoryId = 1 },
            new ProductEntity { Id = 10, Name = "Water Bottle", Price = 24.99m, Description = "Stainless steel water bottle", Modified = DateTime.Now.AddDays(-10), CategoryId = 2 }
        };

   
        return Task.FromResult(products);
    }

    public async Task<ProductEntity?> GetProductById(int id)
    {
        var all = await GetAllProducts();
        var p = all.FirstOrDefault(p => p.Id == id);
        return p;
    }

    public Task<List<ProductEntity>> GetProductsByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }
}
