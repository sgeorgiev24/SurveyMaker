namespace SurveyMaker.Test.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SurveyMaker.Data.Models;
    using SurveyMaker.Services.Implementations;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AnswerServiceTest
    {
        [Fact]
        public async Task AnswerExistAsyncShouldReturnTrueIfAnswerExist()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var answer1 = new AnswerOption { Id = 1 };
            var answer2 = new AnswerOption { Id = 2 };
            var answer3 = new AnswerOption { Id = 3 };

            await db.AddRangeAsync(answer1, answer2, answer3);
            await db.SaveChangesAsync();

            var answerService = new AnswerService(db);

            // Act
            var result = await answerService.AnswerExistAsync(1);

            // Assert
            result
                .ShouldBeEquivalentTo(true);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteAnswer()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var answer1 = new AnswerOption { Id = 1 };
            var answer2 = new AnswerOption { Id = 2 };
            var answer3 = new AnswerOption { Id = 3 };

            await db.AddRangeAsync(answer1, answer2, answer3);
            await db.SaveChangesAsync();

            var answerService = new AnswerService(db);

            // Act
            await answerService.DeleteAsync(2);

            var result = await db.AnswerOptions.ToListAsync();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 1
                    && r.ElementAt(1).Id == 3)
                .And
                .HaveCount(2);
        }
    }
}
