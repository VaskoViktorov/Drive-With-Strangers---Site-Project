namespace DriveWithStrangers.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TripTitleMinLength)]
        [MaxLength(TripTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(TripLocationMinLength)]
        [MaxLength(TripLocationMaxLength)]
        public string StartLocation { get; set; }

        [Required]
        [MinLength(TripTitleMinLength)]
        [MaxLength(TripTitleMaxLength)]
        public string EndLocation { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        [MinLength(TripDescriptionMinLength)]
        [MaxLength(TripDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(TripCarModelMinLength)]
        [MaxLength(TripCarModelMaxLength)]
        public string CarModel { get; set; }

        
        [Range(0,double.MaxValue)]
        public decimal PricePerPassenger { get; set; }

        [Range(1, 10)]
        public int TotalSeats { get; set; }

        public string DriverId { get; set; }

        public User Driver { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UserTrip> Passengers { get; set; } = new List<UserTrip>();
    }
}
