namespace DriveWithStrangers.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly DriveWithStrangersDbContext db;

        public AdminUserService(DriveWithStrangersDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserServiceListingModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserServiceListingModel>()
                .ToListAsync();

        public async Task<Dictionary<string, List<string>>> UsersInRoleAsync()
        {
            var userRoles = await this.db.UserRoles.ToListAsync();

            var userIdWithRolesId = new Dictionary<string, List<string>>();

            foreach (var userRole in userRoles)
            {
                if (!userIdWithRolesId.ContainsKey(userRole.UserId))
                {
                    userIdWithRolesId.Add(userRole.UserId, new List<string>());
                }

                userIdWithRolesId[userRole.UserId].Add(userRole.RoleId);
            }

            var roles = await this.db.Roles.ToListAsync();

            var userIdWithRoleNames = new Dictionary<string, List<string>>();

            foreach (var item in userIdWithRolesId)
            {
                if (!userIdWithRoleNames.ContainsKey(item.Key))
                {
                    userIdWithRoleNames.Add(item.Key, new List<string>());
                }

                foreach (var userRole in item.Value)
                {
                    foreach (var role in roles)
                    {
                        if (userRole == role.Id)
                        {
                            userIdWithRoleNames[item.Key].Add(role.Name);
                            break;                          
                        }
                    }
                }
            }

            return userIdWithRoleNames;
        }
    }
}
