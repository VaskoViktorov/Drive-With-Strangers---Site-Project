using System.Linq;
using AutoMapper;
using DriveWithStrangers.Common.Mapping;
using DriveWithStrangers.Data.Models;

namespace DriveWithStrangers.Services.Models
{
  public class TripWithRateCommentsDetailsServiceModel : TripDetailsServiceModel, IHaveCustomMapping
    {
        public int Rate { get; set; }

        public bool IsRateComment { get; set; }

        public double OverallRate { get; set; }

        public new void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Trip, TripWithRateCommentsDetailsServiceModel>()
                .ForMember(t => t.TakenSeats, cfg => cfg.MapFrom(t => t.Passengers.Count))
                .ForMember(t => t.OverallRate, cfg => cfg.MapFrom(t => t.Comments.Where(c => c.IsRateComment).Select(c => c.Rate).Average()));
    }
}
