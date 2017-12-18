namespace DriveWithStrangers.Services.Models.Trips
{
    using Common.Mapping;
    using Data.Models;

    public class PassangerInTripServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
