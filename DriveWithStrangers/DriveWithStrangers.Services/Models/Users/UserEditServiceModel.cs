namespace DriveWithStrangers.Services.Models.Users
{
    using Common.Mapping;
    using Data.Models;

    public class UserEditServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserFullName { get; set; }

        public int UserAge { get; set; }

        public string UserProfileImgUrl { get; set; }

     
    }
}
