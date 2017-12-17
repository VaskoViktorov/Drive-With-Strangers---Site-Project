namespace DriveWithStrangers.Services
{
    using Models.Users;

    public interface IUserService
    {
        UserDetailsServiceModel ById(string id);

        UserEditServiceModel UserDetailsById(string id);

        void Edit(string id, string email, string userFullName, int userAge, string phone,string userProfileImgUrl);

        bool HasSameEmail(string userModelEmail);
    }
}
