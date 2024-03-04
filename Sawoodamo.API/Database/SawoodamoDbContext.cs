namespace Sawoodamo.API.Database;

public class SawoodamoDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AuthClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AuthLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AuthTokens");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AuthRoleClaims");
        modelBuilder.Entity<IdentityRole>().ToTable("AuthRoles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AuthUserRoles");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SawoodamoDbContext).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<ProductSpec> ProductSpecs { get; set; }
}