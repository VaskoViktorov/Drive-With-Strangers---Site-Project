namespace DriveWithStrangers.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Trips;
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
            string exactAddress,
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
                ExactAddress = exactAddress,
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
                .Where(t => t.StartDate > DateTime.UtcNow)
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db
                .Trips
                .CountAsync();

        public async Task<TripDetailsServiceModel> DetailsByIdAsync(int id)
        {
            var result = await this.db
                .Trips
                .Where(a => a.Id == id)
                .ProjectTo<TripDetailsServiceModel>()
                .FirstOrDefaultAsync();

            result.Comments = result.Comments.Where(c => !c.IsRateComment).ToList();

            return result;
        } 

        public async Task<TripWithRateCommentsDetailsServiceModel> DetailsWithRateCommentsByIdAsync(int id)
        {
          var result= await  this.db
                .Trips
                .Where(a => a.Id == id)
                .ProjectTo<TripWithRateCommentsDetailsServiceModel>()
                .FirstOrDefaultAsync();

            if (result.Comments.Any(c => c.IsRateComment))
            {
                result.OverallRate = result.Comments.Where(c => c.IsRateComment).Select(c => c.Rate).Average();
            }

            return result;
        }
             
        public async Task<bool> JoinAsync(string userId, int tripId)
        {
            var tripInfo = await this.GetTripInfoAsync(userId, tripId);

            if (tripInfo == null)
            {
                return false;
            }

            if (userId == tripInfo.DriverId || tripInfo.UserIsSignedIn)
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
            var tripInfo = await this.GetTripInfoAsync(userId, tripId);

            if (tripInfo == null)
            {
                return false;
            }

            if (tripInfo.StartDate < DateTime.UtcNow ||
                !tripInfo.UserIsSignedIn ||
                userId == tripInfo.DriverId)
            {
                return false;
            }

            var passangerForTrip = await this.db.FindAsync<UserTrip>(userId, tripId);

            this.db.Remove(passangerForTrip);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TripListingServiceModel>> TripsByDriverIdAsync(string id, int page = 1)
            => await this.db
                .Trips
                .Where(t => t.DriverId == id)
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<TripListingServiceModel>> TripsAsPassagerUserIdAsync(string id, int page = 1)
            => await this.db
                .Trips
                .Where(t => t.Passengers.Select(u => u.UserId).Contains(id))
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<TripListingServiceModel>> TripsAsPassagerForRateAsync(string id, int page = 1)
            => await this.db
                .Trips
                .Where(t => t.Passengers.Select(u => u.UserId).Contains(id) && t.StartDate< DateTime.UtcNow)
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

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

        public async Task<TripEditServiceModel> EditByIdAsync(int id)
            => await this.db
                .Trips
                .Where(a => a.Id == id)
                .ProjectTo<TripEditServiceModel>()
                .FirstOrDefaultAsync();

        public async Task EditAsync(int id, string title, string startLocation, string endLocation, DateTime startDate,
            string exactAddress, string description,
            string carModel, decimal pricePerPassenger, int totalSeats, string userId)
        {
            var trip = await this.db.Trips.FindAsync(id);

            if (trip == null)
            {
                return;
            }

            if (trip.DriverId != userId)
            {
                return;
            }

            trip.Title = title;
            trip.StartLocation = startLocation;
            trip.EndLocation = endLocation;
            trip.StartDate = startDate;
            trip.ExactAddress = exactAddress;
            trip.Description = description;
            trip.CarModel = carModel;
            trip.PricePerPassenger = pricePerPassenger;
            trip.TotalSeats = totalSeats;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trip = await this.db.Trips.FindAsync(id);

            if (trip == null)
            {
                return;
            }

            this.db.Trips.Remove(trip);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TripListingServiceModel>> FindAsync(string searchText, bool startLocation, bool endLocation, bool title)
        {
            searchText =searchText.ToLower();

            var result = await this.db
                .Trips
                .Where(t => t.StartDate > DateTime.UtcNow)
                .ProjectTo<TripListingServiceModel>()
                .ToListAsync();

            if (startLocation && endLocation && title)
            {
                result = result.Where(x => x.StartLocation.ToLower().Contains(searchText) || x.EndLocation.ToLower().Contains(searchText) || x.Title.ToLower().Contains(searchText)).ToList();
            }
            else if (startLocation && endLocation)
            {
                result = result.Where(x => x.StartLocation.ToLower().Contains(searchText) || x.EndLocation.ToLower().Contains(searchText)).ToList();
            }
            else if (startLocation && title)
            {
                result = result.Where(x => x.StartLocation.ToLower().Contains(searchText) || x.Title.ToLower().Contains(searchText)).ToList();
            }
            else if (endLocation && title)
            {
                result = result.Where(x => x.EndLocation.ToLower().Contains(searchText) || x.Title.ToLower().Contains(searchText)).ToList();
            }
            else if (startLocation)
            {
                result = result.Where(x => x.StartLocation.ToLower().Contains(searchText)).ToList();
            }
            else if (endLocation)
            {
                result = result.Where(x => x.EndLocation.ToLower().Contains(searchText)).ToList();
            }
            else if (title)
            {
                result = result.Where(x => x.Title.ToLower().Contains(searchText)).ToList();
            }

            return result;
        }

        private async Task<TripInfo> GetTripInfoAsync(string userId, int tripId)
            => await this.db
                .Trips
                .Where(t => t.Id == tripId)
                .Select(c => new TripInfo()
                {
                    StartDate = c.StartDate,
                    DriverId = c.DriverId,
                    UserIsSignedIn = c.Passengers.Any(s => s.UserId == userId)
                })
                .FirstOrDefaultAsync();
    }
}