namespace DriveWithStrangers.Web.Areas.Blog.Models.Articles
{
    using System.Collections.Generic;
    using Services.Blog.Models;

    public class SearchArticleViewModel
    {
        public IEnumerable<BlogArticleListingServiceModel> Articles { get; set; } = new List<BlogArticleListingServiceModel>();

        public string SearchText { get; set; }
    }
}
