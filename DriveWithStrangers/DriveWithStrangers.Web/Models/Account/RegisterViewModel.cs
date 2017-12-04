namespace DriveWithStrangers.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Range(18, 99, ErrorMessage = "You must be at least 18 years old to use this site.")]
        [Display(Name = "Age")]
        public int UserAge { get; set; }

        [Required]
        [MinLength(UserFullNameMinLength)]
        [MaxLength(UserFullNameMaxLength)]
        [Display(Name="Full Name")]
        public string UserFullName { get; set; }

        [MinLength(6)]
        [MaxLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
