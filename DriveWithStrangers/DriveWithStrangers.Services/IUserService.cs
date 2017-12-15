namespace DriveWithStrangers.Services
{
    using Models.Users;

    public interface IUserService
    {
        UserModel ById(string id);

        UserModel UserDetailsById(string id);

        void Edit(string id, string email, string userFullName, int userAge, string phone,string userProfileImgUrl);

        bool HasSameEmail(string userModelEmail);
    }
}
