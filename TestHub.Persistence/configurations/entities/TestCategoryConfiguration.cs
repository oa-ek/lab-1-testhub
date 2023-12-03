using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class TestCategoryConfiguration : IEntityTypeConfiguration<TestCategory>
{
    public void Configure(EntityTypeBuilder<TestCategory> builder)
    {
        builder.HasData(
            new TestCategory
            {
                Id = 1,
                TestId = 1,
                CategoryId = 4
            },
            new TestCategory
            {
                Id = 2,
                TestId = 1,
                CategoryId = 10
            },
            new TestCategory
            {
                Id = 3,
                TestId = 2,
                CategoryId = 7
            },
            new TestCategory
            {
                Id = 4,
                TestId = 2,
                CategoryId = 2
            }
        );
    }
}