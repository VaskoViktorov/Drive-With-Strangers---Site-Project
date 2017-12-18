namespace DriveWithStrangers.Web.Models.Trips
{
    using Services.Models.Trips;
    using System.Collections.Generic;

    public class TripWithRateCommentsDetailsViewModel
    {
        public TripWithRateCommentsDetailsServiceModel Trip { get; set; }

        public IEnumerable<PassangerInTripServiceModel> Passangers { get; set; }

        public bool? UserIsSignedInCourse { get; set; }
    }
}
