namespace DriveWithStrangers.Web.Models.Users
{ 
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(UserFullNameMinLength)]
        [MaxLength(UserFullNameMaxLength)]
        [Display(Name = "Full Name")]
        public string UserFullName { get; set; }

        [Range(18, 120)]
        [Display(Name = "Age")]
        public int UserAge { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Profile Image URL")]
        public string UserProfileImgUrl { get; set; }

        [MaxLength(13)]
        [MinLength(11)]
        [RegularExpression(@"\+\d{10,12}", ErrorMessage = @"Phone must start with a ""+"" and contain between 10 and 12 digits.")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
    }
}
