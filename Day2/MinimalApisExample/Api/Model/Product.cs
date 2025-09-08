namespace Api.Model;


public record CategoryEntity
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public List<ProductEntity> Products { get; init; } = new List<ProductEntity>();
}


public record ProductEntity
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public DateTime Modified { get; init; }
    public int CategoryId { get; init; }
    public CategoryEntity? Category { get; init; }

}

