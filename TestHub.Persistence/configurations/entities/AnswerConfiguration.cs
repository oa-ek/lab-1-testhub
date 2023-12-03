using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasData(
            new Answer
            {
                Id = 1,
                Text = "Грецька",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 1
            },
            new Answer
            {
                Id = 2,
                Text = "Кіммерійська",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 1
            },
            new Answer
            {
                Id = 3,
                Text = "Римська",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 1
            },
            new Answer
            {
                Id = 4,
                Text = "Готська",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 1
            },
            new Answer
            {
                Id = 5,
                Text = "Заснування Києва",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 2
            },
            new Answer
            {
                Id = 6,
                Text = "Велика Літературна Реформа",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 2
            },
            new Answer
            {
                Id = 7,
                Text = "Хрестовий похід",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 2
            },
            new Answer
            {
                Id = 8,
                Text = "Заснування Львова",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 2
            },
            new Answer
            {
                Id = 9,
                Text = "Перша Козацька угода",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 3
            },
            new Answer
            {
                Id = 10,
                Text = "Битва під Грунвальдом",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 3
            },
            new Answer
            {
                Id = 11,
                Text = "Український повстанський рух",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 3
            },
            new Answer
            {
                Id = 12,
                Text = "Укладення Білоцерківського договору",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 3
            },
            new Answer
            {
                Id = 13,
                Text = "Галицько-Волинська держава",
                IsCorrect = true,
                IsStrictText = true,
                QuestionId = 4
            },
            new Answer
            {
                Id = 14,
                Text = "Данилом Романовичем",
                IsCorrect = true,
                IsStrictText = true,
                QuestionId = 4
            },
            new Answer
            {
                Id = 15,
                Text = "Марс",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 5
            },
            new Answer
            {
                Id = 16,
                Text = "Венера",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 5
            },
            new Answer
            {
                Id = 17,
                Text = "Юпітер",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 5
            },
            new Answer
            {
                Id = 18,
                Text = "Земля",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 5
            },
            new Answer
            {
                Id = 19,
                Text = "Канада",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 6
            },
            new Answer
            {
                Id = 20,
                Text = "Бразилія",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 6
            },
            new Answer
            {
                Id = 21,
                Text = "Аргентина",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 6
            },
            new Answer
            {
                Id = 22,
                Text = "Італія",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 6
            },
            new Answer
            {
                Id = 23,
                Text = "Колумбія",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 6
            },
            new Answer
            {
                Id = 24,
                Text = "Червоний",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2F65ce5f4fff1d35b974767bded50f3717_f99219.jpg?alt=media&token=53fe6082-63ba-410f-83b4-117a82a23a45",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 7
            },
            new Answer
            {
                Id = 25,
                Text = "Зелений",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2F%D0%97%D0%B5%D0%BB%D0%B5%D0%BD%D0%B8%D0%B9-0071-1_0884d7.jpg?alt=media&token=7b040b26-c30a-4bf0-98ac-28b0846617d7",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 7
            },
            new Answer
            {
                Id = 26,
                Text = "Синій",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2F%D0%A1%D0%B8%D0%BD%D1%96%D0%B9-0073-1_27c1e6.jpg?alt=media&token=cdff2ce5-c2b6-40b9-94ee-3916434a861a",
                IsCorrect = false,
                IsStrictText = false,
                QuestionId = 7
            },
            new Answer
            {
                Id = 27,
                Text = "Помаранчевий",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2FORANGE_med_d4b2d6.jpg?alt=media&token=226f8a8f-4ccc-4ce3-9bf1-da2db2889dbd",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 7
            },
            new Answer
            {
                Id = 28,
                Text = "Жовтий",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/testhub-aspdotnetwithmvc.appspot.com/o/images%2F5UYjcAn_43f234.png?alt=media&token=6dcac393-e627-4e60-9a6a-d3cb3d872ae5",
                IsCorrect = true,
                IsStrictText = false,
                QuestionId = 7
            }
        );
    }
}