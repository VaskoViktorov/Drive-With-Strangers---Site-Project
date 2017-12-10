namespace DriveWithStrangers.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
   {
       Task<IEnumerable<AdminUserServiceListingModel>> AllAsync();

       Task<Dictionary<string, List<string>>> UsersInRoleAsync();
   }
}
