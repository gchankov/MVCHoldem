namespace MVCHoldem.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        private const string UserNameDisplayName = "User name";
        private const int UserNameMaxLength = 20;
        private const int UserNameMinLength = 5;
        private const string UserNameErrorMessage = "The {0} must be at least {2} characters long.";

        private const string EmailDisplayName = "Email";

        private const int PasswordMaxLength = 100;
        private const int PasswordMinLength = 6;
        private const string PasswordErrorMessage = "The {0} must be at least {2} characters long.";
        private const string PasswordDisplayName = "Password";

        private const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";
        private const string ConfirmPasswordDisplayName = "Confirm password";

        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = UserNameErrorMessage, MinimumLength = UserNameMinLength)]
        [Display(Name = UserNameDisplayName)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = EmailDisplayName)]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = PasswordErrorMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordDisplayName)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ConfirmPasswordDisplayName)]
        [Compare(PasswordDisplayName, ErrorMessage = ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}