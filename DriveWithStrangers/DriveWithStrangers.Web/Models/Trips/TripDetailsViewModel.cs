using System.Collections.Generic;

namespace DriveWithStrangers.Web.Models.Trips
{
    using Services.Models;

    public class TripDetailsViewModel
    {
        public TripDetailsServiceModel Trip { get; set; }

        public IEnumerable<PassangerInTripServiceModel> Passangers { get; set; }

        public bool? UserIsSignedInCourse { get; set; }
    }
}
