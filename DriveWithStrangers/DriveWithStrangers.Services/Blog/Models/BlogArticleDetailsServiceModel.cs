namespace DriveWithStrangers.Services.Blog.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class BlogArticleDetailsServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, BlogArticleDetailsServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.User.UserName));
    }
}
