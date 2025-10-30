namespace Api.Model.Config;


public record YVCollectionConfig
{
    public required string DatabaseName { get; init; }
    public required string CollectionName { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public required Address Address { get; init; }
    public required string[] AllowUsers { get; init; }
}

public record Address
{
    public required string Host { get; init; }
    public int Port { get; init; }
}

