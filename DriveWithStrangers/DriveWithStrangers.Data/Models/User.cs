namespace DriveWithStrangers.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserFullNameMinLength)]
        [MaxLength(UserFullNameMaxLength)]
        public string UserFullName { get; set; }

        [Range(18,120)]
        public int UserAge { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UserTrip> Trips { get; set; } = new List<UserTrip>();

        public List<Trip> MyTrips { get; set; } = new List<Trip>();

        public List<Article> Articles { get; set; } = new List<Article>();

    }
}
