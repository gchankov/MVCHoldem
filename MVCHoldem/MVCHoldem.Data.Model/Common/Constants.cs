namespace MVCHoldem.Data.Models.Common
{
    public static class Constants
    {
        public const int PostTitleMinLength = 5;
        public const int PostTitleMaxLength = 50;
        public const string PostTitleErrorMessage = "Incorrect post title!";

        public const int PostDesctiptionMinLength = 5;
        public const int PostDescriptionMaxLength = 100;
        public const string PostDescriptionErrorMessage = "Incorrect post description!";

        public const int PostContentMinLength = 100;
        public const int PostContentMaxLength = 30000;
        public const string PostContentErrorMessage = "Incorrect post content!";
    }
}