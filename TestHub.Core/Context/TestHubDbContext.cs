using Microsoft.EntityFrameworkCore;
using TestHub.Core.Models;
using TestHub.Core.Enum;

namespace TestHub.Core.Context;

public partial class TestHubDbContext : DbContext
{
    public TestHubDbContext()
    {
    }

    public TestHubDbContext(DbContextOptions<TestHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<StatusSessionQuestion> StatusSessionQuestions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestCategory> TestCategories { get; set; }

    public virtual DbSet<TestMetadata> TestMetadata { get; set; }

    public virtual DbSet<TestSession> TestSessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=TestHubDb;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answer__3214EC07DCBEB09A");

            entity.ToTable("Answer");

            entity.HasIndex(e => e.Image, "UQ__Answer__3294EFD547D97E91").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Image)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.IsStrictText).HasColumnName("isStrictText");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Text)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070B33831E");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Title, "UQ__Category__2CB664DC6C1971A6").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC078A670D31");

            entity.ToTable("Question");

            entity.HasIndex(e => e.Image, "UQ__Question__3294EFD504A554B5").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Test");

            entity.HasOne(d => d.Type).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_QuestionType");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC074E605F90");

            entity.ToTable("QuestionType");

            entity.HasIndex(e => e.Type, "UQ__Question__F9B8A48B62D55D75").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusSessionQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusSe__3214EC07A7BA35CB");

            entity.ToTable("StatusSessionQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.StatusSessionQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusSessionQuestion_Question");

            entity.HasOne(d => d.Session).WithMany(p => p.StatusSessionQuestions)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusSessionQuestion_TestSession");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3214EC07B3DDA46A");

            entity.ToTable("Test");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasConversion(
                    v => v.ToString(),
                    v => (TestStatus) System.Enum.Parse(typeof(TestStatus), v)
                    );


            entity.HasOne(d => d.Owner).WithMany(p => p.Tests)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_User");
        });

        modelBuilder.Entity<TestCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestCate__3214EC07C9169216");

            entity.ToTable("TestCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.TestCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestCategory_Category");

            entity.HasOne(d => d.Test).WithMany(p => p.TestCategories)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestCategory_Test");
        });

        modelBuilder.Entity<TestMetadata>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestMeta__3214EC0713326B82");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Like)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.Test).WithMany(p => p.TestMetadata)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestMetadata_Test");

            entity.HasOne(d => d.User).WithMany(p => p.TestMetadata)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestMetadata_User");
        });

        modelBuilder.Entity<TestSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestSess__3214EC07C6DAD699");

            entity.ToTable("TestSession");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FinishedAt).HasColumnType("datetime");
            entity.Property(e => e.IsTraining).HasColumnName("isTraining");
            entity.Property(e => e.StartedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Test).WithMany(p => p.TestSessions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestSession_Test");

            entity.HasOne(d => d.User).WithMany(p => p.TestSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestSession_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC072F16F37C");

            entity.ToTable("User");

            entity.HasIndex(e => e.Name, "UQ__User__737584F633AAFF38").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.DeleteAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateAd).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
