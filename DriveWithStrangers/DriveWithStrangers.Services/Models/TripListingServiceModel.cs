namespace DriveWithStrangers.Services.Models
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class TripListingServiceModel : IMapFrom<Trip>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public decimal PricePerPassenger { get; set; }

        public int TotalSeats { get; set; }

        public int TakenSeats { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Trip, TripListingServiceModel>()
                .ForMember(a => a.TakenSeats, cfg => cfg.MapFrom(a => a.Passengers.Count));
    }
}
