namespace Api.Database;

public class YvDataContext(DbContextOptions<YvDataContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductEntity>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<CategoryEntity>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        // Seed Categories
        modelBuilder.Entity<CategoryEntity>().HasData(
            new CategoryEntity { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
            new CategoryEntity { Id = 2, Name = "Household", Description = "Household items and accessories" },
            new CategoryEntity { Id = 3, Name = "Furniture", Description = "Office and home furniture" },
            new CategoryEntity { Id = 4, Name = "Books", Description = "Educational and reference books" },
            new CategoryEntity { Id = 5, Name = "Stationery", Description = "Office and writing supplies" }
        );

        // Seed Products
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity { Id = 1, Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", Modified = new DateTime(2024, 12, 20), CategoryId = 1 },
            new ProductEntity { Id = 2, Name = "Smartphone", Price = 699.99m, Description = "Latest model smartphone", Modified = new DateTime(2024, 12, 22), CategoryId = 1 },
            new ProductEntity { Id = 3, Name = "Coffee Mug", Price = 12.99m, Description = "Ceramic coffee mug", Modified = new DateTime(2024, 12, 24), CategoryId = 2 },
            new ProductEntity { Id = 4, Name = "Desk Chair", Price = 249.99m, Description = "Ergonomic office chair", Modified = new DateTime(2024, 12, 18), CategoryId = 3 },
            new ProductEntity { Id = 5, Name = "Headphones", Price = 149.99m, Description = "Wireless noise-canceling headphones", Modified = new DateTime(2024, 12, 23), CategoryId = 1 },
            new ProductEntity { Id = 6, Name = "Book", Price = 19.99m, Description = "Programming guide", Modified = new DateTime(2024, 12, 21), CategoryId = 4 },
            new ProductEntity { Id = 7, Name = "Monitor", Price = 299.99m, Description = "24-inch LED monitor", Modified = new DateTime(2024, 12, 19), CategoryId = 1 },
            new ProductEntity { Id = 8, Name = "Pen Set", Price = 29.99m, Description = "Premium pen collection", Modified = new DateTime(2024, 12, 17), CategoryId = 5 },
            new ProductEntity { Id = 9, Name = "Tablet", Price = 399.99m, Description = "10-inch tablet", Modified = new DateTime(2024, 12, 16), CategoryId = 1 },
            new ProductEntity { Id = 10, Name = "Water Bottle", Price = 24.99m, Description = "Stainless steel water bottle", Modified = new DateTime(2024, 12, 15), CategoryId = 2 }
        );
    }
}
