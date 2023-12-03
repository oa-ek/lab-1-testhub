using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasData(
            new Question
            {
                Id = 1,
                Title = "Яка цивілізація існувала на території України в добу бронзи?",
                Description = "Оберіть один варіант відповіді",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2F124807.010_111049.png?alt=media&token=e255952a-d349-4732-b577-afb570318c35",
                AssociatedTestId = 1,
                TypeId = 1
            },
            new Question
            {
                Id = 2,
                Title = "Яка з наступних подій відбулася в середньовіччі на території України?",
                Description = "Виберіть всі правильні варіанти відповіді",
                AssociatedTestId = 1,
                TypeId = 2
            },
            new Question
            {
                Id = 3,
                Title = "Які події стали переломними в історії Козацької Речі Посполитої?",
                Description = "Виберіть всі правильні варіанти відповіді",
                AssociatedTestId = 1,
                TypeId = 2
            }, 
            new Question
            {
                Id = 4,
                Title = "Пізніше, у середньовіччі, на території України виникла держава під назвою \"[Галицько-Волинська держава]\", заснована князем [Данилом Романовичем].",
                Description = "Заповніть пропуски",
                AssociatedTestId = 1,
                TypeId = 4
            },
            new Question
            {
                Id = 5,
                Title = "Яка планета є найближчою до Сонця?",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2Fmaxresdefault_6586f4.jpg?alt=media&token=f3a400ce-3fc8-46a5-b111-57bb6631b6f2",
                AssociatedTestId = 2,
                TypeId = 1
            },
            new Question
            {
                Id = 6,
                Title = "Які з нижче перелічених країн розташовані в Південній Америці?",
                Description = "Виберіть всі правильні відповіді",
                AssociatedTestId = 2,
                TypeId = 2
            },
            new Question
            {
                Id = 7,
                Title = "Які з нижче перелічених кольорів є теплими кольорами?",
                Description = "Виберіть всі правильні відповіді",
                AssociatedTestId = 2,
                TypeId = 2
            }
        );
    }
}