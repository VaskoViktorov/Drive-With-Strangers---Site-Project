namespace DriveWithStrangers.Data.Models
{
    public class UserTrip
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
