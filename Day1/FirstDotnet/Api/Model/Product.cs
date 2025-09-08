namespace Api.Model;

public record Product
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
}

public interface IEntity
{
    int Id { get; }
    string Name { get; }
}
public record ProductAsRecord(int Id, string Name, decimal Price, string? Description) : IEntity;

