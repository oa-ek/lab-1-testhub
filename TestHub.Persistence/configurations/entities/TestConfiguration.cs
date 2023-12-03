using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasData(
            new Test
            {
                Id = 1,
                Title = "Історія України. Первісні часи",
                Description = "Цей тест призначений для визначення знань про історію України в перші історичні періоди - первісні часи.",
                Duration = 15,
                OwnerId = 1,
                Status = "Draft",
                IsPublic = true
            },
            new Test
            {
                Id = 2,
                Title = "Природознавство 6 клас",
                Description = "Тест містить 5 питань, що охоплюють різні теми.",
                Duration = 5,
                OwnerId = 1,
                Status = "Created",
                IsPublic = true
            }
        );
    }
}