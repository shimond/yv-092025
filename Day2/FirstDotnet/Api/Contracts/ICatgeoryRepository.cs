using Api.Model;

namespace Api.Contracts;

public interface IProductsRepository
{
    Task<List<ProductEntity>> GetAllProducts();
    Task<ProductEntity?> GetProductById(int id);
    Task<List<ProductEntity>> GetProductsByCategoryId(int categoryId);
}
