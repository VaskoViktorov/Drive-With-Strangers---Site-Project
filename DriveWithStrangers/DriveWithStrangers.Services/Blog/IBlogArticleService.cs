namespace DriveWithStrangers.Services.Blog
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page =1);

        Task<int> TotalAsync();

        Task<BlogArticleDetailsServiceModel> ById(int id);

        Task Create(DateTime releaseDate, string title, string content, string authorId, string shortContent, string imageUrl);

        Task Edit(int id, string title, string content, DateTime releaseDate, string userId, string shortContent, string imageUrl);

        Task Delete(int id);
    }
}
