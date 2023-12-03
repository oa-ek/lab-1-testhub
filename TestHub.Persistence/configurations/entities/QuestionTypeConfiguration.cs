using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
{
    public void Configure(EntityTypeBuilder<QuestionType> builder)
    {
        builder.HasData(
            new QuestionType
            {
                Id = 1,
                Type = "single"
            },
            new QuestionType
            {
                Id = 2,
                Type = "multiple"
            },
            new QuestionType
            {
                Id = 3,
                Type = "binary"
            },
            new QuestionType
            {
                Id = 4,
                Type = "blank"
            }
        );
    }
}