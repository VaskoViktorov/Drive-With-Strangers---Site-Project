namespace DriveWithStrangers.Web.Areas.Admin.Models.Users
{
    using Common.Mapping;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class AdminUsersListingViewModel : IMapFrom<User>

    {
        public IEnumerable<AdminUserServiceListingModel> Users { get; set; }

       public Dictionary<string, List<string>> UserRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
