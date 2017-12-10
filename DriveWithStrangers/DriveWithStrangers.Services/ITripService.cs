namespace DriveWithStrangers.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITripService
    {
        Task CreateAsync(
            string title,
            string startLocation,
            string endLocation,
            DateTime startDate,
            string description,
            string carModel,
            decimal pricePerPassenger,
            int totalSeats,
            string userId);

        Task<IEnumerable<TripListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<TripDetailsServiceModel> ByIdAsync(int id);

        Task<bool> SignOutUserAsync(string userId, int tripId);

        Task<bool> JoinAsync(string userId, int tripId);

        Task<bool> UserIsSignedForTripAsync(string userId, int tripId);

        Task<IEnumerable<PassangerInTripServiceModel>> PassangersInTripAsync(int tripId);
    }
}
