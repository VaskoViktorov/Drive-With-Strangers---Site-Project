namespace DriveWithStrangers.Test.Services
{
    using Data;
    using Data.Models;
    using DriveWithStrangers.Services.Implementations;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class TripServiceTest
    {
        public TripServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task FindAsyncShouldReturnCorrectResultWithFilterAndOrder()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstTrip= new Trip { Id = 1, Title = "First" };
            var secondTrip = new Trip { Id = 2, Title = "Second" };
            var thirdTrip = new Trip { Id = 3, Title = "Third" };

            db.AddRange(firstTrip, secondTrip, thirdTrip);

            await db.SaveChangesAsync();

            var tripService = new TripService(db);

            // Act
            var result = await tripService.FindAsync("t",true,true,true);

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                    && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task SignUpUserAsyncShouldSaveCorrectDataWithValidTripIdAndUserId()
        {
            // Arrange
            var db = this.GetDatabase();

            const int tripId = 1;
            const string userId = "TestUserId";

            var trip = new Trip()
            {
                Id = tripId,
                StartDate = DateTime.MaxValue,
                Passengers = new List<UserTrip>()
            };

            db.Add(trip);

            await db.SaveChangesAsync();

            var tripService = new TripService(db);

            // Act
            var result = await tripService.JoinAsync(userId, tripId);
            var savedEntry = db.Find<UserTrip>(userId, tripId);

            // Assert
            result
                .Should()
                .Be(true);

            savedEntry
                .Should()
                .NotBeNull();
        }

        private DriveWithStrangersDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<DriveWithStrangersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DriveWithStrangersDbContext(dbOptions);
        }
    }
}
