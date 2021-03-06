﻿namespace DriveWithStrangers.Web.Controllers
{
    using Services.Html;
    using Infrastructure.Extensions;
    using Models.Comments;
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class CommentsController : Controller
    {
        private const string ModelName = "Comment";

        private readonly ICommentService comments;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService html;

        public CommentsController(ICommentService comments, UserManager<User> userManager, IHtmlService html)
        {
            this.comments = comments;
            this.userManager = userManager;
            this.html = html;
        }

        [Authorize]
        public IActionResult Create()
            => this.View();

        [HttpPost]
        [Authorize]
        [ValidateModelState]
        public async Task<IActionResult> Create(int id, CommentFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(this.User);
            var userName = this.userManager.GetUserName(this.User);

            await this.comments.CreateAsync(model.Title, model.Content, userId, userName, id);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataCreateCommentText, ModelName));

            return this.RedirectToAction("Details", "Trips", new { id });
        }

        [Authorize]
        public IActionResult RateCommentCreate()
            => this.View();

        [HttpPost]
        [Authorize]
        [ValidateModelState]
        public async Task<IActionResult> RateCommentCreate(int id, RateCommentFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(this.User);
            var userName = this.userManager.GetUserName(this.User);

            if (!await this.comments.CreateRateCommentAsync(model.Title, model.Content, model.Rate, userId, userName, id))
            {
                return this.NotFound();
            }

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataCreateCommentText, ModelName));

            return this.RedirectToAction("DetailsRate", "Trips", new { id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var comment = await this.comments.EditByIdAsync(id);

            if (comment == null)
            {
                return this.NotFound();
            }

            if (userId != comment.UserId && !this.User.IsInRole(WebConstants.AdministratorRole))
            {
                return this.RedirectToAction("Details", "Trips", new { id = comment.TripId });
            }

            return this.View(new CommentFormModel
            {
                Title = comment.Title,
                Content = comment.Content,
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, int tripId, CommentFormModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            var username = this.userManager.GetUserName(this.User);
     
            await this.comments.EditAsync(id,
                model.Title,
                model.Content,
                userId,
                username);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataEditCommentText, ModelName));

            return this.RedirectToAction("Details", "Trips", new { id = tripId });
        }



        public IActionResult Delete(int id, int tripId)
            => this.View(new List<int> { id, tripId });

        public async Task<IActionResult> Destroy(int id, int commentId)
        {
            await this.comments.DeleteAsync(commentId);

            this.TempData.AddSuccessMessage(string.Format(WebConstants.TempDataDeleteCommentText, ModelName));

            return this.RedirectToAction("Details", "Trips", new { id });
        }

    }
}
