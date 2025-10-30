namespace Api.Services;

public class EFProductsRepository(YvDataContext dataContext) : IProductsRepository
{
    public async Task<List<ProductEntity>> GetAllProducts()
    {
        var products = await dataContext.Products.ToListAsync();
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
