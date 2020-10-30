namespace D72TP1P3.Models.DataModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User {
        public enum UserType { Administrator, Donator, Member }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessage = "Username must start with a letter, have between 4 and 15 characters, using only letters and digits.")]
        [Display(Name = "UserName", Description = "Your UserName.")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "Your Password.")]
        [RegularExpression(@"^[A-Za-z0-9!@/$%#&*?\-""']+$", ErrorMessage= "Use letters, digits and symbols (!@/$%#&*?-\"')")]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Description = "Your email.")]
        public string Email { get; set; }

        [EnumDataType(typeof(UserType))]
        public UserType Type { get; set; } = UserType.Member;

        [InverseProperty("Users")]
        public virtual ICollection<TvShow> Favorites { get; set; } = new HashSet<TvShow>();

        [InverseProperty("Users")]
        public virtual ICollection<Episode> History { get; set; } = new HashSet<Episode>();

       // public override bool IsUserInRole(string username, string roleName) => throw new NotImplementedException();

    }
}