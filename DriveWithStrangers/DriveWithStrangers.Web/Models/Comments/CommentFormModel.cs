namespace DriveWithStrangers.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CommentFormModel
    {
        [Required]
        [MinLength(CommentTitleMinLength)]
        [MaxLength(CommentTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(CommentContentMinLength)]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; }
    }
}
