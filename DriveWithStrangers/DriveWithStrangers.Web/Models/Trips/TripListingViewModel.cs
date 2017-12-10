namespace DriveWithStrangers.Web.Models.Trips
{
    using System;
    using System.Collections.Generic;
    using Services;
    using Services.Models;


    public class TripListingViewModel
    {
        public IEnumerable<TripListingServiceModel> Trips { get; set; }

        public int TotalTrips { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalTrips / ServicesConstants.PageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
