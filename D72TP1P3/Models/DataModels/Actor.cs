namespace D72TP1P3.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DisplayColumn(displayColumn: "Name")]
    public class Actor {
        [Key]
        public int ActorId { get; set; }

        [Required, MaxLength(50), Index(IsUnique = true)]
        public string Name { get; set; }

        [InverseProperty("Actor")]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        [NotMapped]
        public string BasePath { get => $"/Content/Media/Actors"; }

        [NotMapped]
        public string Image { get => $"{this.BasePath}/{this.Name.Replace(' ', '_').Replace(":", "").Replace("/", " ").TrimEnd(new[] { '.' })}.jpg"; }
    }
}
