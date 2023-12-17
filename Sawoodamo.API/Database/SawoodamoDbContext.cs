namespace Sawoodamo.API.Database;

public class SawoodamoDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : AuditableDbContext(options, httpContextAccessor)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("Claims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("Logins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("Tokens");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SawoodamoDbContext).Assembly);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
}