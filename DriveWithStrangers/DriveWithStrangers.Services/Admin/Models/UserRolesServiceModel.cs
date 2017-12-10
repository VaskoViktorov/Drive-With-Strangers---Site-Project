using DriveWithStrangers.Common.Mapping;
using Microsoft.AspNetCore.Identity;

namespace DriveWithStrangers.Services.Admin.Models
{
   public class UserRolesServiceModel : IMapFrom<IdentityUserRole<string>>
    {
        public string RoleId { get; set; }
    }
}
