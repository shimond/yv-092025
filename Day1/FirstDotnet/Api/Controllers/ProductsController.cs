using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IOutputCacheStore _cacheStore;

    public ProductsController(IOutputCacheStore cacheStore)
    {
        _cacheStore = cacheStore;
    }

    [HttpGet]
    [OutputCache(Duration = 30, Tags = new[] { "products", "all" })]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(5000); // Simulate slow operation
        return Ok(new[] { "Product1", "Product2", "Product3" });
    }

    [HttpPost]
    [OutputCache(Duration = 0)] // No caching for POST
    public async Task<IActionResult> Post([FromBody] string product)
    {
        // Invalidate cache when new product is added
        await _cacheStore.EvictByTagAsync("products", CancellationToken.None);
        
        // In a real application, you would save the product to a database
        return CreatedAtAction(nameof(Get), new { id = 4 }, product);
    }

    [HttpGet]
    [Route("{id}")]
    [OutputCache(Duration = 60, Tags = new[] { "products", "single" })]
    public IActionResult GetById(int id)
    {
        // In a real application, you would retrieve the product from a database
        return Ok($"Product{id}");
    }

    [HttpDelete]
    [Route("cache/clear")]
    public async Task<IActionResult> ClearCache()
    {
        await _cacheStore.EvictByTagAsync("products", CancellationToken.None);
        return Ok("Cache cleared for products");
    }

    [HttpDelete]
    [Route("cache/clear/{tag}")]
    public async Task<IActionResult> ClearCacheByTag(string tag)
    {
        await _cacheStore.EvictByTagAsync(tag, CancellationToken.None);
        return Ok($"Cache cleared for tag: {tag}");
    }
}
