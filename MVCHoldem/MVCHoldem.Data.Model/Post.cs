namespace MVCHoldem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MVCHoldem.Data.Models.Abstracts;

    public class Post : DataModel
    {
        private const int PostTitleMinLength = 5;
        private const int PostTitleMaxLength = 50;
        private const string PostTitleErrorMessage = "Incorrect post title!";

        private const int PostDesctiptionMinLength = 5;
        private const int PostDescriptionMaxLength = 100;
        private const string PostDescriptionErrorMessage = "Incorrect post description!";

        private const int PostContentMinLength = 100;
        private const int PostContentMaxLength = 30000;
        private const string PostContentErrorMessage = "Incorrect post content!";

        [Required]
        [StringLength(PostTitleMaxLength, MinimumLength = PostTitleMinLength, ErrorMessage = PostTitleErrorMessage)]
        public string Title { get; set; }

        [Required]
        [StringLength(PostDescriptionMaxLength, MinimumLength = PostDesctiptionMinLength, ErrorMessage = PostDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        [StringLength(PostContentMaxLength, MinimumLength = PostContentMinLength, ErrorMessage = PostContentErrorMessage)]
        public string Content { get; set; }

        public virtual User Author { get; set; }
    }
}
