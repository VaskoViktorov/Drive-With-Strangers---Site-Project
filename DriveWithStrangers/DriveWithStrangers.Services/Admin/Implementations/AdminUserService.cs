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
    }
}
