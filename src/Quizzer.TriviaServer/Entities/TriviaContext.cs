using Microsoft.EntityFrameworkCore;

namespace Quizzer.TriviaServer.Entities
{
    public class TriviaContext : DbContext
    {
        public TriviaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany<Question>(c => c.Questions)
                .WithOne(q => q.Category)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany<Answer>(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasKey(a => new { a.QuestionId, a.Order });

            #region Seed Data
            // Seed Data
            modelBuilder.Entity<Category>()
                .HasData(new List<Category>()
                {
                    new Category
                    {
                        Id = 1,
                        Name = "Information Technology",
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Geography"
                    }
                });

            modelBuilder.Entity<Question>()
                .HasData(new List<Question>()
                {
                    new Question
                    {
                        Id = 1,
                        Text = "Which year was the IBM PC introduced?",
                        CategoryId = 1,
                        QuestionType = Question.OpenTriviaQuestionType.MultipleChoice,
                        Difficulty = Question.OpenTriviaDifficulty.Medium,
                    },
                    new Question
                    {
                        Id = 2,
                        Text = "Which year was the Macintosh introduced?",
                        CategoryId = 1,
                        QuestionType = Question.OpenTriviaQuestionType.MultipleChoice,
                        Difficulty = Question.OpenTriviaDifficulty.Medium,
                    }
                });

            modelBuilder.Entity<Answer>()
                .HasData(new List<Answer>
                    {
                        new Answer()
                        {
                            QuestionId = 1,
                            Order = 0,
                            IsCorrect = false,
                            Text = "1992"
                        },
                        new Answer()
                        {
                            QuestionId = 1,
                            Order = 1,
                            IsCorrect = true,
                            Text = "1981"
                        },
                            new Answer()
                        {
                            QuestionId = 1,
                            Order = 2,
                            IsCorrect = false,
                            Text = "1979"
                        },
                        new Answer()
                        {
                            QuestionId = 1,
                            Order = 3,
                            IsCorrect = false,
                            Text = "1984"
                        },
                        new Answer()
                        {
                            QuestionId = 2,
                            Order = 0,
                            IsCorrect = false,
                            Text = "1990"
                        },
                        new Answer()
                        {
                            QuestionId = 2,
                            Order = 1,
                            IsCorrect = false,
                            Text = "1981"
                        },
                            new Answer()
                        {
                            QuestionId = 2,
                            Order = 2,
                            IsCorrect = false,
                            Text = "1979"
                        },
                        new Answer()
                        {
                            QuestionId = 2,
                            Order = 3,
                            IsCorrect = true,
                            Text = "1984"
                        },
                    });
            #endregion
        }
    }
}
