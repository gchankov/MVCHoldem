namespace MVCHoldem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MVCHoldem.Data.Models.Abstracts;
    using MVCHoldem.Data.Models.Common;

    public class Post : DataModel
    {
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

        public virtual User Author { get; set; }
    }
}
