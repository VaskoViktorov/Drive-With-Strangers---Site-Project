namespace DriveWithStrangers.Web.Models.Trips
{
    using Services.Models;
    using System.Collections.Generic;

    public class SearchTripViewModel
    {
        public IEnumerable<TripListingServiceModel> Trips { get; set; } = new List<TripListingServiceModel>();

        public string SearchText { get; set; }
    }
}
