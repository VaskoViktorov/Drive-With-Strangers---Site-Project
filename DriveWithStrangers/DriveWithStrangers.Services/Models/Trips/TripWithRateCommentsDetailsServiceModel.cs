namespace DriveWithStrangers.Services.Models.Trips
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class TripWithRateCommentsDetailsServiceModel : TripDetailsServiceModel, IHaveCustomMapping
    {
        public int Rate { get; set; }

        public bool IsRateComment { get; set; }

        public double OverallRate { get; set; }

        public new void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Trip, TripWithRateCommentsDetailsServiceModel>()
                .ForMember(t => t.TakenSeats, cfg => cfg.MapFrom(t => t.Passengers.Count));   
    }
}
