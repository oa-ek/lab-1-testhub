using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 1,
                Email = "user1@example.com",
                Password = "$2a$11$X4BnEeGGHouHYjehJ.MNqu16R9DEm9oQ20Aoxa9jb8fDQdy.wt7cG",
                Name = "User 1",
                Role = "User",
                Comment = "This is User 1",
                Token = "vYbFwWVY5Hd1ixiMT6OTu44Zr2hDStwqeWUte8TBdxCIl2ZcZ+LyQBonzFPcX7jxuwqzJvOGqs5Qhnr1t4UTgw==",
                IsEmailVerified = true,
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now.AddYears(1)
            }
        );
    }
}