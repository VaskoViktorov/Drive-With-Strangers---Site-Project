namespace DriveWithStrangers.Web.Areas.Blog.Models.Articles
{
    using Services;
    using Services.Blog.Models;
    using System;
    using System.Collections.Generic;

    public class ArticleListingViewModel
    {

        public IEnumerable<BlogArticleListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages
            => (int)Math.Ceiling((double)this.TotalArticles / ServicesConstants.PageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage 
            => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == TotalPages? TotalPages: this.CurrentPage + 1;
    }
}
