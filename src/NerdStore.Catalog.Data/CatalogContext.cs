using NerdStore.Core.Abstractions;

namespace NerdStore.Catalog.Data;
public class CatalogContext : DbContext, IUnitOfWork
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }


    public async Task<bool> CommitAsync()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Created_At") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("Created_At").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("Created_At").IsModified = false;
        }

        return await base.SaveChangesAsync() > 0;
    }
}
