namespace DriveWithStrangers.Services.Models
{
    using System.Collections.Generic;
    using Data.Models;

    public class TripDetailsServiceModel : TripListingServiceModel
    {
        public string Description { get; set; }

        public string CarModel { get; set; }

        public User Driver { get; set; }

        public string ExactAddress { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();     
    }
}
