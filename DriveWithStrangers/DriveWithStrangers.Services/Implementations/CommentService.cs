namespace DriveWithStrangers.Services.Implementations
{
    using Data;
    using Data.Models;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Models.Comments;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly DriveWithStrangersDbContext db;

        public CommentService(DriveWithStrangersDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string title, string content, string userId, string userName, int id)
        {
            var comment = new Comment
            {
                Title = title,
                Content = content,               
                CreateDate = DateTime.UtcNow,
                UserId = userId,
                CommenterName = userName,
                TripId = id
            };

            this.db.Add(comment);

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await this.db.Comments.FindAsync(id);

            if (comment == null)
            {
                return;
            }

            this.db.Comments.Remove(comment);

            await this.db.SaveChangesAsync();
        }

        public async Task<CommentEditServiceModel> EditByIdAsync(int id)
            => await this.db
                .Comments
                .Where(a => a.Id == id)
                .ProjectTo<CommentEditServiceModel>()
                .FirstOrDefaultAsync();

        public async Task EditAsync(int id, string title, string content, string userId,string username)
        {
            var comment = await this.db.Comments.FindAsync(id);

            if (comment == null)
            {
                return;
            }

            if (comment.UserId != userId)
            {
                return;
            }
                       
            comment.Title = title;
            comment.Content = content;
            comment.EditorUsername = username;
            comment.IsEdited = true;
            comment.EditDate = DateTime.UtcNow;

            await this.db.SaveChangesAsync();
        }
    }
}
