namespace D72TP1P3.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    [CustomValidation(typeof(SignUp), "ValidateSignUp")]
    public class SignUp {
        public static ValidationResult ValidateSignUp(SignUp i) {
            if (!i.IAgree) { return new ValidationResult("YOU MUST AGREE!"); }
            return ValidationResult.Success;
        }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessage = "Username must start with a letter, have between 4 and 15 characters, using only letters and digits.")]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Views.UserStrings))]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Views.UserStrings))]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Views.UserStrings))]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]

        [Display(Name = "Email", ResourceType = typeof(Resources.Views.UserStrings))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "IAgree", ResourceType = typeof(Resources.Views.UserStrings))]
        public bool IAgree { get; set; }

        //I agree to the site conditions.
    }
}