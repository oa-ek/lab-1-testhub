﻿using System.Reflection;
using Domain.common;

namespace TestHub.Persistence
{
    public class TestHubDbContext : DbContext
    {
        public TestHubDbContext()
        {
        }

        public TestHubDbContext(DbContextOptions<TestHubDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                entry.Entity.LastModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<Answer>? Answers { get; set; }

        public virtual DbSet<Category>? Categories { get; set; }

        public virtual DbSet<Question>? Questions { get; set; }

        public virtual DbSet<QuestionType>? QuestionTypes { get; set; }

        public virtual DbSet<StatusSessionQuestion>? StatusSessionQuestions { get; set; }

        public virtual DbSet<Test>? Tests { get; set; }

        public virtual DbSet<TestCategory>? TestCategories { get; set; }

        public virtual DbSet<TestMetadata>? TestMetadata { get; set; }

        public virtual DbSet<TestSession>? TestSessions { get; set; }

        public virtual DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                "Server=.;Database=TestHubDb;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(BaseAuditableEntity)));

            foreach (var entityType in entityTypes)
            {
                var method = modelBuilder.GetType().GetMethod("Entity")?.MakeGenericMethod(entityType);
                var entityBuilder = method?.Invoke(modelBuilder, null);

                var idProperty = entityType.GetProperty("Id");
                var valueGeneratedOnAddMethod = entityBuilder?.GetType().GetMethod("Property")
                    ?.MakeGenericMethod(idProperty?.PropertyType!);
                var propertyBuilder = valueGeneratedOnAddMethod?.Invoke(entityBuilder, new object[] { idProperty! });

                propertyBuilder?.GetType().GetMethod("ValueGeneratedOnAdd")?.Invoke(propertyBuilder, null);
            }
        }
    }
}