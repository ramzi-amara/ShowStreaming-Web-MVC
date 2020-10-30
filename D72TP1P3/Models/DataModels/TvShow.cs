namespace D72TP1P3.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [DisplayColumn(displayColumn: "Title")]
    public class TvShow {
        public enum TVParentalGuidelines {
            [Display(Name = "TV-Y")]
            TVY,
            [Display(Name = "TV-Y7")]
            TVY7,
            [Display(Name = "TV-G")]
            TVG,
            [Display(Name = "TV-PG")]
            TVPG,
            [Display(Name = "TV-14")]
            TV14,
            [Display(Name = "TV-MA")]
            TVMA
        }
        [Key]
        [Display(Name = "TvShowId")]
        public int TvShowId { get; set; }

        [Required, MaxLength(100), Index(IsUnique = true)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Rating")]
        public decimal Rating { get; set; }

        [Required, MaxLength(1500), DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        [Display(Name = "Parental Guideline")]
        public TVParentalGuidelines TVParentalGuideline { get; set; }

        [Required, MaxLength(12), Index(IsUnique = true), ScaffoldColumn(false)]
        public string IMDBId { get; set; }

        [Required, MaxLength(12), Index(IsUnique = true), ScaffoldColumn(false)]
        public string TVDBId { get; set; }

        [InverseProperty("TvShows")]
        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        [InverseProperty("TvShow")]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        [ForeignKey("Studio")]
        public int StudioId { get; set; }

        [ForeignKey("StudioId")]
        public virtual Studio Studio { get; set; }

        [InverseProperty("TvShow")]
        public virtual ICollection<Season> Seasons { get; set; } = new HashSet<Season>();

        [InverseProperty("Favorites")]
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        [NotMapped]
        public int EpisodeCount { get => this.Seasons.Sum(s => s.Episodes.Count); }

        [NotMapped]
        public string BasePath { get => $"/Content/Media/TvShows/{this.Title.Replace(":", "").Replace("/", " ").TrimEnd(new[] { '.' })}"; }

        [NotMapped]
        public string Image { get => $"{this.BasePath}/poster.jpg"; }
    }
}
