namespace DriveWithStrangers.Services.Models.Comments
{
    using Common.Mapping;
    using Data.Models;

    public class CommentEditServiceModel : IMapFrom<Comment>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsEdited { get; set; }

        public string EditorUsername { get; set; }

        public string UserId { get; set; }

        public string TripId { get; set; }
    }
}
