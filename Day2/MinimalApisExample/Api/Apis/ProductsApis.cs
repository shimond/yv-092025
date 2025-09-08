namespace Api.Apis;

public static class ProductsApis
{
    public static void MapProductsApis(this IEndpointRouteBuilder app)
    {
        var productsGroup = app.MapGroup("/api/products");
        productsGroup.MapGet("", GetAllProducts);
        productsGroup.MapGet("{id}",GetProductById);
    }

    static async Task<Ok<List<ProductDto>>> GetAllProducts(IProductsRepository productsRepository)
    {
        var productsEntities = await productsRepository.GetAllProducts();
        var productDtos = productsEntities.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Description)).ToList();
        return TypedResults.Ok(productDtos);
    }

    static async Task<Results<NotFound, Ok<ProductDto>>> GetProductById(int id, IProductsRepository productsRepository)
    {
        var productEntity = await productsRepository.GetProductById(id);
        if (productEntity == null)
        {
            return TypedResults.NotFound(); 
        }
        var productDto = new ProductDto(productEntity.Id, productEntity.Name, productEntity.Price, productEntity.Description);
        return TypedResults.Ok(productDto); 
    }
}
