using Api.Model;

namespace Api.Contracts;

public interface ICategoriesRepository
{
    Task<List<CategoryEntity>> GetCategories();
    Task<CategoryEntity?> GetCategoryById(int categoryId);
}
