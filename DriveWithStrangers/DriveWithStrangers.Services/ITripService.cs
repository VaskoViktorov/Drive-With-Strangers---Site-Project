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
            string exactAddress,
            string description,
            string carModel,
            decimal pricePerPassenger,
            int totalSeats,
            string userId);

        Task<IEnumerable<TripListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<TripDetailsServiceModel> DetailsByIdAsync(int id);

        Task<bool> SignOutUserAsync(string userId, int tripId);

        Task<bool> JoinAsync(string userId, int tripId);

        Task<bool> UserIsSignedForTripAsync(string userId, int tripId);

        Task<IEnumerable<PassangerInTripServiceModel>> PassangersInTripAsync(int tripId);

        Task<TripEditServiceModel> EditByIdAsync(int id);

        Task EditAsync(
            int id,
            string title,
            string startLocation, 
            string endLocation, 
            DateTime startDate,
            string exactAddress,
            string description,
            string carModel,
            decimal pricePerPassenger,
            int totalSeats,
            string userId);

        Task DeleteAsync(int id);

        Task<IEnumerable<TripListingServiceModel>> FindAsync(
            string searchText,
            bool startLocation,
            bool endLocation,
            bool title);
    }
}
