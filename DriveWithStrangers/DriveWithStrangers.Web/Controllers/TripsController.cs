
namespace DriveWithStrangers.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trips;
    using Services;
    using System.Threading.Tasks;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Infrastructure.Extensions;

    public class TripsController : Controller
    {
        private readonly ITripService trips;
        private readonly UserManager<User> userManager;

        public TripsController(ITripService trips, UserManager<User> userManager)
        {
            this.trips = trips;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
            => this.View(new TripListingViewModel
            {
                Trips = await this.trips.AllAsync(page),
                TotalTrips = await this.trips.TotalAsync(),
                CurrentPage = page
            });

        [Authorize]
        public IActionResult Create()
            => this.View();

        [HttpPost]
        [Authorize]
        [ValidateModelState]
        public async Task<IActionResult> Create(TripFormModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.trips.CreateAsync(
                model.Title,
                model.StartLocation,
                model.EndLocation,
                model.StartDate,
                model.Description,
                model.CarModel,
                model.PricePerPassenger,
                model.TotalSeats, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new TripDetailsViewModel()
            {
                Trip = await this.trips.ByIdAsync(id),
                Passangers = await this.trips.PassangersInTripAsync(id)
            };

            if (model.Trip == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(this.User);

                model.UserIsSignedInCourse =
                    await this.trips.UserIsSignedForTripAsync(userId, id);
            }

            return this.View(model);
        }
            

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var success = await this.trips.JoinAsync(userId, id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage("You signed up successfully!");

            return this.RedirectToAction(nameof(this.Details), new {id});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var success = await this.trips.SignOutUserAsync(userId, id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddWarningMessage("You signed out successfully.");

            return this.RedirectToAction(nameof(this.Details), new {id});
        }
    }
}
