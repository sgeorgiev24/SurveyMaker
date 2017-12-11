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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
