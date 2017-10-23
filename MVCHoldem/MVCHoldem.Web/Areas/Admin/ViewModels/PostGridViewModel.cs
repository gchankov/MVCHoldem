namespace MVCHoldem.Web.Areas.Admin.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Data.Models.Common;
    using MVCHoldem.Web.Contracts;

    public class PostGridViewModel : IMap<Post>, IHaveCustomMappings
    {
        [Required]
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }

        [Required]
        [StringLength(Constants.PostTitleMaxLength,
            MinimumLength = Constants.PostTitleMinLength,
            ErrorMessage = Constants.PostTitleErrorMessage)]
        public string Title { get; set; }

        [Required]
        [StringLength(Constants.PostDescriptionMaxLength,
            MinimumLength = Constants.PostDesctiptionMinLength,
            ErrorMessage = Constants.PostDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        [StringLength(Constants.PostContentMaxLength,
            MinimumLength = Constants.PostContentMinLength,
            ErrorMessage = Constants.PostContentErrorMessage)]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        
        public bool IsDeleted { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, PostGridViewModel>()
                .ForMember(postGridViewModel => postGridViewModel.Id, cfg => cfg.MapFrom(post => post.Id.ToString()))
                .ForMember(postGridViewModel => postGridViewModel.Author, cfg => cfg.MapFrom(post => post.Author.UserName));
        }
    }
}