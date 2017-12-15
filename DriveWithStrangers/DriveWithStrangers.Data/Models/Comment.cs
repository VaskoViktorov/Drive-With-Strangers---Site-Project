namespace DriveWithStrangers.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CommentTitleMinLength)]
        [MaxLength(CommentTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(CommentContentMinLength)]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        [Range(1,5)]
        public int Rate { get; set; }

        public string CommenterName { get; set; }

        public bool IsEdited { get; set; }

        public string EditorUsername { get; set; }

        public DateTime EditDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
