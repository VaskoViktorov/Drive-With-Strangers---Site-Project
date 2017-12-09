namespace DriveWithStrangers.Web.Areas.Blog.Models.Articles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ArticleFormModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(ArticleShortContentMaxLength)]
        public string ShortContent { get; set; }

        [Required]
        [MaxLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
