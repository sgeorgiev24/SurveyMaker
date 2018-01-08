namespace SurveyMaker.Test.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SurveyMaker.Data.Models;
    using SurveyMaker.Services.Implementations;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class PollServiceTest
    {
        public PollServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task DeleteAsyncShouldDeletePoll()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var poll1 = new Poll { Id = 1 };
            var poll2 = new Poll { Id = 2 };
            var poll3 = new Poll { Id = 3 };

            await db.AddRangeAsync(poll1, poll2, poll3);
            await db.SaveChangesAsync();

            var pollService = new PollService(db);

            // Act
            await pollService.DeleteAsync(2);

            var result = await db.Polls.ToListAsync();

            // Assert
            result
                .Should()
                .Match(p => p.ElementAt(0).Id == 1
                    && result.ElementAt(1).Id == 3)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task EditAsyncShouldEditPollNameAndDescription()
        {
            // Arrange
            var db = Tests.GetDatabase();

            const string newName = "New Poll";
            const string newDescription = "New Description";

            var poll = new Poll
            {
                Id = 1,
                Name = "Poll",
                Description = "Description"
            };

            await db.AddAsync(poll);
            await db.SaveChangesAsync();

            var pollService = new PollService(db);

            // Act
            await pollService.EditAsync(1, newName, newDescription);
            var result = db.Polls.Find(1);

            // Assert
            result.Id.Should().Be(1);
            result.Name.Should().Be(newName);
            result.Description.Should().Be(newDescription);
        }

        [Fact]
        public async Task PollsByUserIdAsyncShouldReturnCorrectPollsWithFilter()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var user1Id = "user1";
            var user2Id = "user2";

            var poll1 = new Poll
            {
                Id = 1,
                Name = "Poll",
                AuthorId = user1Id
            };

            var poll2 = new Poll
            {
                Id = 2,
                Name = "Another",
                AuthorId = user1Id
            };

            var poll3 = new Poll
            {
                Id = 3,
                Name = "Poll 2",
                AuthorId = user2Id
            };

            await db.AddRangeAsync(poll1, poll2, poll3);
            await db.SaveChangesAsync();

            var pollService = new PollService(db);

            // Act
            var polls = await pollService.PollsByUserIdAsync(user1Id, null);
            var pollsWithSearch = await pollService.PollsByUserIdAsync(user1Id, "poll");

            polls
                .Should()
                .Match(p => p.ElementAt(0).Id == 2
                    && p.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);

            pollsWithSearch
                .Should()
                .Match(p => p.ElementAt(0).Id == 1)
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task PollExistAsyncShouldReturnTrueIfPollExist()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var poll = new Poll { Id = 1 };

            await db.AddAsync(poll);
            await db.SaveChangesAsync();

            var pollService = new PollService(db);

            // Act
            var result = await pollService.PollExistAsync(1);

            // Assert
            result
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task PollByIdAsyncShouldFindPollWithGivenId()
        {
            // Arrange
            var db = Tests.GetDatabase();

            const string pollName = "Test Poll Name";

            var poll = new Poll
            {
                Id = 1,
                Name = pollName
            };

            await db.AddAsync(poll);
            await db.SaveChangesAsync();

            var pollService = new PollService(db);

            // Act
            var result = await pollService.PollByIdAsync(1);

            // Assert
            result.Id.Should().Be(1);
            result.Name.Should().Be(pollName);
        }
    }
}
