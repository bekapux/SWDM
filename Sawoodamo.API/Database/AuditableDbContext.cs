namespace Sawoodamo.API.Database;

public abstract class AuditableDbContext : IdentityUserContext<Sawoodamo.API.Database.Entities.User>
{
    private readonly string? userId;

    protected AuditableDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        userId = httpContextAccessor.HttpContext?.User.FindFirst(Constants.CustomClaimTypes.Uid)?.Value ?? "Test";
    }

    public virtual async Task<int> SaveChangesAsync<ID>(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity<ID>>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.LastModifiedDate = DateTime.Now;
            entry.Entity.LastModifiedBy = userId;

            if (entry.State != EntityState.Added) continue;
            entry.Entity.DateCreated = DateTime.Now;
            entry.Entity.CreatedBy = userId;
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}