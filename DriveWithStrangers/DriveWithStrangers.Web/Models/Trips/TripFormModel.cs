namespace DriveWithStrangers.Web.Models.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class TripFormModel
    {
        [Required]
        [MinLength(TripTitleMinLength)]
        [MaxLength(TripTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(TripLocationMinLength)]
        [MaxLength(TripLocationMaxLength)]
        [Display(Name = "Start Location")]
        public string StartLocation { get; set; }

        [Required]
        [MinLength(TripLocationMinLength)]
        [MaxLength(TripLocationMaxLength)]
        [Display(Name = "End Location")]
        public string EndLocation { get; set; }
        
        [Display(Name = "Trip Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [MinLength(TripExactAddressMinLength)]
        [MaxLength(TripExactAddressMaxLength)]
        [Display(Name = "Exact Address")]
        public string ExactAddress { get; set; }

        [Required]
        [MinLength(TripDescriptionMinLength)]
        [MaxLength(TripDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(TripCarModelMinLength)]
        [MaxLength(TripCarModelMaxLength)]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }


        [Range(0, double.MaxValue)]
        [Display(Name = "Price Per Passenger")]
        public decimal PricePerPassenger { get; set; }

        [Range(1, 10)]
        [Display(Name="Total Seats")]
        public int TotalSeats { get; set; }
    }
}
