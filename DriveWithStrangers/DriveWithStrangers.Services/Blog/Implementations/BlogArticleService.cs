namespace DriveWithStrangers.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServicesConstants;

    public class BlogArticleService : IBlogArticleService
    {

        private readonly DriveWithStrangersDbContext db;

        public BlogArticleService(DriveWithStrangersDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page = 1)
        => await this.db
            .Articles
            .OrderByDescending(a => a.CreateDate)
            .Where(a => a.ReleaseDate <= DateTime.UtcNow)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ProjectTo<BlogArticleListingServiceModel>()
            .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db
                .Articles
                .Where(a => a.ReleaseDate <= DateTime.UtcNow)
                .CountAsync();

        public async Task<BlogArticleDetailsServiceModel> ById(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<BlogArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task Create(DateTime releaseDate, string title, string content, string authorId, string shortContent, string imageUrl)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                ReleaseDate = releaseDate,
                CreateDate = DateTime.UtcNow,
                ShortContent = shortContent,
                ImageUrl = imageUrl,
                UserId = authorId
            };

            this.db.Add(article);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string title, string content, DateTime releaseDate, string userId, string shortContent, string imageUrl)
        {
            var article = await this.db.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            if (article.UserId != userId)
            {
                return;
            }

            article.Title = title;
            article.Content = content;
            article.ReleaseDate = releaseDate;
            article.ShortContent = shortContent;
            article.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var article = await this.db.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            this.db.Articles.Remove(article);

            await this.db.SaveChangesAsync();
        }
    }
}
