namespace DriveWithStrangers.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trips;
    using Services;
    using System.Linq;
    using System.Threading.Tasks;

    public class TripsController : Controller
    {
        private const string ModelName = "Trip";

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
                model.ExactAddress,
                model.Description,
                model.CarModel,
                model.PricePerPassenger,
                model.TotalSeats, userId);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataCreateCommentText, ModelName));

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new TripDetailsViewModel()
            {
                Trip = await this.trips.DetailsByIdAsync(id),
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
        public async Task<IActionResult> DetailsRate(int id)
        {
            var model = new TripWithRateCommentsDetailsViewModel
            {
                Trip = await this.trips.DetailsWithRateCommentsByIdAsync(id),
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

                if (!model.Passangers.Any(t => t.Id == userId))
                {
                    return this.RedirectToAction(nameof(this.Details), new { id });
                }
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

            return this.RedirectToAction(nameof(this.Details), new { id });
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

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize]
        public async Task<IActionResult> DriverTrips(string id, int page = 1)
            => this.View(new TripListingViewModel
            {
                Trips = await this.trips.TripsByDriverIdAsync(id, page),
                TotalTrips = await this.trips.TotalAsync(),
                CurrentPage = page
            });

        [Authorize]
        public async Task<IActionResult> PassagerTrips(string id, int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId != id)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(new TripListingViewModel
            {
                Trips = await this.trips.TripsAsPassagerUserIdAsync(id, page),
                TotalTrips = await this.trips.TotalAsync(),
                CurrentPage = page
            });
        }

        [Authorize]
        public async Task<IActionResult> TripsForRate(string id, int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId != id)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(new TripListingViewModel
            {
                Trips = await this.trips.TripsAsPassagerForRateAsync(id, page),
                TotalTrips = await this.trips.TotalAsync(),
                CurrentPage = page
            });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var trip = await this.trips.EditByIdAsync(id);

            if (trip == null)
            {
                return this.NotFound();
            }

            if (userId != trip.DriverId)
            {
                return this.RedirectToAction(nameof(this.Details), new { id });
            }

            if (trip.TakenSeats > 0)
            {
                this.TempData.AddWarningMessage("You can't edit trip that already has passengers.");

                return this.RedirectToAction(nameof(this.Details), new { id });
            }

            return this.View(new TripFormModel
            {
                Title = trip.Title,
                StartLocation = trip.StartLocation,
                EndLocation = trip.EndLocation,
                StartDate = trip.StartDate,
                ExactAddress = trip.ExactAddress,
                Description = trip.Description,
                CarModel = trip.CarModel,
                PricePerPassenger = trip.PricePerPassenger,
                TotalSeats = trip.TotalSeats,
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, TripFormModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.trips.EditAsync(id,
                model.Title,
                model.StartLocation,
                model.EndLocation,
                model.StartDate,
                model.ExactAddress,
                model.Description,
                model.CarModel,
                model.PricePerPassenger,
                model.TotalSeats,
                userId);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataEditCommentText, ModelName));

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        public IActionResult Delete(int id)
            => this.View(id);

        public async Task<IActionResult> Destroy(int id)
        {
            await this.trips.DeleteAsync(id);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataDeleteCommentText, ModelName));

            return this.RedirectToAction(nameof(this.Index));
        }


        public async Task<IActionResult> Search(SearchTripFormModel model)
        {
            if (StringExtensions.IsNullOrWhiteSpace(model.SearchText))
            {
                this.TempData.AddWarningMessage("No search text was written.");

                return this.RedirectToAction(nameof(this.Index));
            }

            if (!model.SearchInEndLocations && !model.SearchInStartLocations && !model.SearchInTitles)
            {
                this.TempData.AddWarningMessage("No search condition was added.");

                return this.RedirectToAction(nameof(this.Index));
            }

            var viewModel = new SearchTripViewModel
            {
                SearchText = model.SearchText,
                Trips = await this.trips.FindAsync(
                    model.SearchText,
                    model.SearchInStartLocations,
                    model.SearchInEndLocations,
                    model.SearchInTitles)
            };

            return this.View(viewModel);
        }
    }
}
