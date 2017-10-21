namespace MVCHoldem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Web.Contracts;

    public class PostDetailsViewModel : IMap<Post>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, PostDetailsViewModel>()
                .ForMember(mostRecentPostViewModel => mostRecentPostViewModel.Author, cfg => cfg.MapFrom(post => post.Author.UserName))
                .ForMember(mostRecentPostViewModel => mostRecentPostViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        }
    }
}