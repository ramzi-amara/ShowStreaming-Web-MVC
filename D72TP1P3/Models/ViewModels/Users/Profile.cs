namespace D72TP1P3.Models.ViewModels.User {
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Linq;
    using D72TP1P3.Models.DataModels;
    using System;

    [CustomValidation(typeof(Profile), "ValidateProfile")]
    public class Profile {
        public static ValidationResult ValidateProfile(Profile profil) {
            if (!string.IsNullOrEmpty(profil.NewPassword)) {
                TVShowDb db = new TVShowDb();
                User u = db.Users.Find(int.Parse(HttpContext.Current.User.Identity.Name));
                if (u.Password != profil.OldPassword) {
                    return new ValidationResult("Your old password does not match.  Your password has not been changed.", new[] { "" });
                }
            }
            return ValidationResult.Success;
        }

        [MaxLength(15)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword" , ResourceType = typeof(Resources.Views.UserStrings))]
        public string OldPassword { get; set; }

        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Views.UserStrings))]
        [Compare("ConfirmNewPassword")]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessage = "Use letters, digits and symbols (!@/$%#&*?-\"')")]
        public string NewPassword { get; set; }

        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Resources.Views.UserStrings))]
        [Compare("NewPassword")]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessage = "Use letters, digits and symbols (!@/$%#&*?-\"')")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Views.UserStrings))]
        public string Email { get; set; }
    }
}