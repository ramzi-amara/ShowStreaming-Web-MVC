namespace D72TP1P3.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using D72TP1P3.Models.DataModels;
    using System.Linq;

    [CustomValidation(typeof(Login), "ValidateLogin")]
    public class Login {
        public static ValidationResult ValidateLogin(Login login) {
            TVShowDb db = new TVShowDb();
            User user = db.Users.SingleOrDefault(u=>u.UserName == login.UserName);
            if (user == null) { return new ValidationResult("Bad username or password"); }
            if (user.Password != login.Password) { return new ValidationResult("Bad username or password"); }
            return ValidationResult.Success;
        }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Views.UserStrings), Description = "Your UserName")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Views.UserStrings), Description = "Your password.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Views.UserStrings))]
        public bool RememberMe { get; set; }
    }
}