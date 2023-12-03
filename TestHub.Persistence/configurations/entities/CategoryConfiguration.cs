using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHub.Persistence.configurations.entities;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = 1,
                Title = "Географія та подорожі"
            },
            new Category
            {
                Id = 2,
                Title = "Здоров'я та медицина"
            },
            new Category
            {
                Id =  3,
                Title = "Історія та культура"
            },
            new Category
            {
                Id = 4,
                Title = "Математика та статистика"
            },
            new Category
            {
                Id = 5,
                Title = "Мови та лінгвістика"
            },
            new Category
            {
                Id = 6,
                Title = "Наука та технологія"
            },
            new Category
            {
                Id = 7,
                Title = "Освіта та навчання"
            },
            new Category
            {
                Id = 8,
                Title = "Психологія та особистісний розвиток"
            },
            new Category
            {
                Id = 9,
                Title = "Розваги та мистецтво"
            },
            new Category
            {
                Id = 10,
                Title = "Спорт та фізична підготовка"
            }
        );
    }
}