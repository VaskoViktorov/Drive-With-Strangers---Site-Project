namespace DriveWithStrangers.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServicesConstants;

    public class TripService : ITripService
    {
        private readonly DriveWithStrangersDbContext db;

        public TripService(DriveWithStrangersDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(
            string title,
            string startLocation,
            string endLocation,
            DateTime startDate,
            string description,
            string carModel,
            decimal pricePerPassenger,
            int totalSeats,
            string userId)
        {
            var trip = new Trip
            {
                Title = title,
                StartLocation = startLocation,
                EndLocation = endLocation,
                StartDate = startDate,
                Description = description,
                CarModel = carModel,
                PricePerPassenger = pricePerPassenger,
                TotalSeats = totalSeats,
                DriverId = userId
            };

            this.db.Add(trip);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TripListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Trips
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db
                .Trips
                .CountAsync();

        public async Task<TripDetailsServiceModel> ByIdAsync(int id)
            => await this.db
                .Trips
                .Where(a => a.Id == id)
                .ProjectTo<TripDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> JoinAsync(string userId, int tripId)
        {
            var tripInfo = await this.GetTripInfoAsync(userId, tripId);

            if (tripInfo == null || tripInfo.UserIsSignedIn)
            {
                return false;
            }

            var passagerInTrip = new UserTrip()
            {
                UserId = userId,
                TripId = tripId
            };

            this.db.Add(passagerInTrip);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutUserAsync(string userId, int tripId)
        {
            var courseInfo = await this.GetTripInfoAsync(userId, tripId);

            if (courseInfo == null || courseInfo.StartDate < DateTime.UtcNow || !courseInfo.UserIsSignedIn)
            {
                return false;
            }

            var passangerForTrip = await this.db.FindAsync<UserTrip>(userId, tripId);

            this.db.Remove(passangerForTrip);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserIsSignedForTripAsync(string userId, int tripId)
            => await this.db
                .Trips
                .AnyAsync(c => c.Id == tripId && c.Passengers.Any(s => s.UserId == userId));

        public async Task<IEnumerable<PassangerInTripServiceModel>> PassangersInTripAsync(int tripId)
            => await this.db
                .Trips
                .Where(c => c.Id == tripId)
                .SelectMany(c => c.Passengers.Select(s => s.User))
                .ProjectTo<PassangerInTripServiceModel>()
                .ToListAsync();

        private async Task<TripInfo> GetTripInfoAsync(string userId, int tripId)
            => await this.db
                .Trips
                .Where(t => t.Id == tripId)
                .Select(c => new TripInfo()
                {
                    StartDate = c.StartDate,
                    UserIsSignedIn = c.Passengers.Any(s => s.UserId == userId)
                })
                .FirstOrDefaultAsync();
    }
}
