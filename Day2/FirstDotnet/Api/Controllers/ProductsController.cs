using Api.Contracts;
using Api.Model.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        var productsEntities = await productsRepository.GetAllProducts();
        var productDtos = productsEntities.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Description));
        return Ok(productDtos); // 200 + data
        //return Ok("ma nisgar!");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var productEntity = await productsRepository.GetProductById(id);
        if (productEntity == null)
        {
            return NotFound(); // 404
        }
        var productDto = new ProductDto(productEntity.Id, productEntity.Name, productEntity.Price, productEntity.Description);
        return Ok(productDto); // 200 + data
    }

    [HttpGet("CountByCategory/{categoryId}")]
    public async Task<ActionResult<int>> GetProductsCountByCategory(int categoryId)
    {
        var category = await categoriesRepository.GetCategoryById(categoryId);
        if (category == null)
        {
            return NotFound(); // 404
        }
        
        return Ok(category.Products.Count); // 200 + data
    }


}
