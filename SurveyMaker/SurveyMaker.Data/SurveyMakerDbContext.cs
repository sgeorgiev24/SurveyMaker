namespace SurveyMaker.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SurveyMaker.Data.Models;

    public class SurveyMakerDbContext : IdentityDbContext<User>
    {
        public SurveyMakerDbContext(DbContextOptions<SurveyMakerDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnswerOption> AnswerOptions { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<AnswerOption>()
                .HasOne(ao => ao.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(ao => ao.QuestionId);

            builder
                .Entity<Question>()
                .HasOne(q => q.Poll)
                .WithMany(p => p.Questions)
                .HasForeignKey(q => q.PollId);

            builder
                .Entity<Poll>()
                .HasOne(p => p.Author)
                .WithMany(a => a.Polls)
                .HasForeignKey(p => p.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
