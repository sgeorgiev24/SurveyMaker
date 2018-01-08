namespace SurveyMaker.Test.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SurveyMaker.Data.Models;
    using SurveyMaker.Services.Implementations;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class QuestionServiceTest
    {
        [Fact]
        public async Task AnswersCountAsyncShouldReturnNumberOfQuestionAnswers()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var answers = new List<AnswerOption>();
            answers.Add(new AnswerOption { Id = 1, QuestionId = 1 });
            answers.Add(new AnswerOption { Id = 2, QuestionId = 1 });
            answers.Add(new AnswerOption { Id = 3, QuestionId = 1 });
            answers.Add(new AnswerOption { Id = 4, QuestionId = 1 });
            answers.Add(new AnswerOption { Id = 5, QuestionId = 1 });

            await db.AddRangeAsync(answers);
            await db.SaveChangesAsync();

            var question = new Question
            {
                Id = 1
            };

            var questionService = new QuestionService(db);

            // Act
            var result = await questionService.AnswersCountAsync(1);

            // Assert
            result
                .Should()
                .Be(5);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewQuestion()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var answers = new List<string>();
            answers.Add("answer1");
            answers.Add("answer2");
            answers.Add("answer3");
            answers.Add("answer4");
            answers.Add("answer5");


            var questionTitle = "Test question title";

            var poll = new Poll { Id = 1 };

            var questionService = new QuestionService(db);

            // Act
            // var result = await questionService.CreateAsync(poll.Id, questionTitle, answers);
        }
    }
}
