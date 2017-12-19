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
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Short Content")]
        [MaxLength(ArticleShortContentMaxLength)]
        public string ShortContent { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [MaxLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
