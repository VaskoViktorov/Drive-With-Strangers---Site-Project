namespace DriveWithStrangers.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        public string Content { get; set; }

        [Required]
        [MaxLength(ArticleImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ArticleShortContentMaxLength)]
        public string ShortContent { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
