namespace DriveWithStrangers.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class TripEditServiceModel : IMapFrom<Trip>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public DateTime StartDate { get; set; }

        public string ExactAddress { get; set; }

        public string Description { get; set; }

        public string CarModel { get; set; }

        public decimal PricePerPassenger { get; set; }

        public int TotalSeats { get; set; }

        public string DriverId { get; set; }

        public int TakenSeats { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Trip, TripEditServiceModel>()
                .ForMember(a => a.TakenSeats, cfg => cfg.MapFrom(a => a.Passengers.Count));
    }
}
