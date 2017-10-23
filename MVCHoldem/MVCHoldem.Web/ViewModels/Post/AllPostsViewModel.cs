namespace MVCHoldem.Web.ViewModels.Post
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Web.Contracts;

    public class AllPostsViewModel : IMap<Post>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, AllPostsViewModel>()
                .ForMember(allPostsViewModel => allPostsViewModel.Author, cfg => cfg.MapFrom(post => post.Author.UserName))
                .ForMember(allPostsViewModel => allPostsViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        }
    }
}