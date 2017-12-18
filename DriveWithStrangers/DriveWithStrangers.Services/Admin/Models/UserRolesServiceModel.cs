namespace DriveWithStrangers.Services.Admin.Models
{
    using Common.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserRolesServiceModel : IMapFrom<IdentityUserRole<string>>
    {
        public string RoleId { get; set; }
    }
}
