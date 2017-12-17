namespace DriveWithStrangers.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class RateCommentFormModel : CommentFormModel
    {
        [Range(1, 5)]
        public int Rate { get; set; } = 0;
    }
}
