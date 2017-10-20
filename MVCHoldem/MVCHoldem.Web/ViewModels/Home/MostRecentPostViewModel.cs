namespace MVCHoldem.Web.ViewModels.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Web.Contracts;

    public class MostRecentPostViewModel : IMap<Post>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorEmail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, MostRecentPostViewModel>()
                .ForMember(mostRecentPostViewModel => mostRecentPostViewModel.AuthorEmail, cfg => cfg.MapFrom(post => post.Author.Email))
                .ForMember(mostRecentPostViewModel => mostRecentPostViewModel.PostedOn, cfg => cfg.MapFrom(post => post.CreatedOn));
        }
    }
}