namespace DriveWithStrangers.Services
{
    using System.Threading.Tasks;
    using Models.Comments;

    public interface ICommentService
    {
        Task CreateAsync(string title, string content, string userId, string userName, int id);

        Task<bool> CreateRateCommentAsync(string title, string content,int rate, string userId, string userName, int id);

        Task DeleteAsync(int id);

        Task<CommentEditServiceModel> EditByIdAsync(int id);

        Task EditAsync(int id, string title, string content, string userId,string username);
    }
}
