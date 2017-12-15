namespace DriveWithStrangers.Web.Models.Trips
{
    using System.ComponentModel.DataAnnotations;

    public class SearchTripFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Titles")]
        public bool SearchInTitles { get; set; } = true;

        [Display(Name = "Start Locations")]
        public bool SearchInStartLocations { get; set; } = true;

        [Display(Name = "End Locations")]
        public bool SearchInEndLocations { get; set; } = true;
    }
}
