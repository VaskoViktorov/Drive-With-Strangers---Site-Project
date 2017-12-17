using DriveWithStrangers.Services.Models;
using System.Collections.Generic;

namespace DriveWithStrangers.Web.Models.Trips
{
    public class TripWithRateCommentsDetailsViewModel
    {
        public TripWithRateCommentsDetailsServiceModel Trip { get; set; }

        public IEnumerable<PassangerInTripServiceModel> Passangers { get; set; }

        public bool? UserIsSignedInCourse { get; set; }
    }
}
