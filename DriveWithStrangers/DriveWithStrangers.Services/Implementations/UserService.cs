namespace DriveWithStrangers.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.Users;
    using System.Linq;

    public class UserService : IUserService
    {
        public readonly DriveWithStrangersDbContext db;

        public UserService(DriveWithStrangersDbContext db)
        {
            this.db = db;
        }

        public UserModel ById(string id)
            => this.db
                .Users
                .Where(c => c.Id == id)
                .ProjectTo<UserModel>()
                .FirstOrDefault();

        public UserModel UserDetailsById(string id)
            => this.db
                .Users
                .Where(c => c.Id == id)
                .ProjectTo<UserModel>()
                .FirstOrDefault();

        public void Edit(string id, string email, string userFullName, int userAge, string phone, string userProfileImgUrl)
        {
            var user = this.db.Users.Find(id);

            user.Email = email;
            user.UserFullName = userFullName;
            user.UserAge = userAge;
            user.UserProfileImgUrl = userProfileImgUrl;
            user.PhoneNumber = phone;
            this.db.SaveChanges();
        }

        public bool HasSameEmail(string userModelEmail)
            => this.db.Users.Any(u => u.Email == userModelEmail);
    }
}
