
using Api.Contracts;
using Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class EFProductsRepository(YvDataContext dataContext) : IProductsRepository
{
    public Task<List<ProductEntity>> GetAllProducts()
    {
        var products = dataContext.Products.ToListAsync();
        return products;
    }

    public async Task<ProductEntity?> GetProductById(int id)
    {
        var product = await dataContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);  

        return product;
    }

    public async Task<List<ProductEntity>> GetProductsByCategoryId(int categoryId)
    {
        var result = await  dataContext.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return result;
    }
}
