using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Seed;

public class UserSeed : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(
            new User
            {
                Id = "83630a13-fe8f-4d4c-bff4-f5d322f8ea5f",
                Firstname = "Beka",
                Lastname = "Pukhashvili",
                Email = "beka.pukhashvili@gmail.com",
                NormalizedEmail = "BEKA.PUKHASHVILI@GMAIL.COM",
                IsDeleted = false,
                IsAdmin = true,
                DateRegistered = DateTime.UtcNow,
                EmailConfirmed = true,
                UserName = "beka.pukhashvili",
                PhoneNumber = "551345679",
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                IsActive = true,
                LockoutEnabled = true,
                PasswordHash = hasher.HashPassword(null, "asdASD123!"),
                NormalizedUserName = "BEKA.PUKHASHVILI",
            }
        );
    }
}
