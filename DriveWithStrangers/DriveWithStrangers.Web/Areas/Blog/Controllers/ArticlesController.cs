namespace DriveWithStrangers.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Blog;
    using Services.Html;
    using System.Threading.Tasks;


    public class ArticlesController : BaseController
    {
        private readonly IBlogArticleService articles;
        private readonly IHtmlService html;
        private readonly UserManager<User> userManager;

        public ArticlesController(IBlogArticleService articles, IHtmlService html, UserManager<User> userManager)
        {
            this.articles = articles;
            this.html = html;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        => this.View(new ArticleListingViewModel
        {
            Articles = await this.articles.AllAsync(page),
            TotalArticles = await this.articles.TotalAsync(),
            CurrentPage = page
        });


        public IActionResult Create()
            => this.View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(ArticleFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(this.User);

            await this.articles.Create(model.ReleaseDate, model.Title, model.Content, userId, model.ShortContent,model.ImageUrl);

            return this.RedirectToAction(nameof(this.Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.articles.ById(id));


        public async Task<IActionResult> Edit(int id)
        {
            var article = await this.articles.ById(id);

            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(new ArticleFormModel
            {
                Title = article.Title,
                Content = article.Content,
                ReleaseDate = article.ReleaseDate,
                ShortContent = article.ShortContent,
                ImageUrl = article.ImageUrl
            });
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, ArticleFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(this.User);

            await this.articles.Edit(id, model.Title, model.Content, model.ReleaseDate, userId, model.ShortContent,model.ImageUrl);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Delete(int id)
            => this.View(id);

        public async Task<IActionResult> Destroy(int id)
        {
            await this.articles.Delete(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
