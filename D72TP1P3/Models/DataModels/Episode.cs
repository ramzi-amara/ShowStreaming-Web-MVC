namespace D72TP1P3.Models.DataModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using D72TP1P3.Resources.Models;
    public class Episode {
        [Key]
        public int EpisodeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Aired { get; set; }

        [Range(1, 255)]
        public byte Number { get; set; }

        [Required, MaxLength(2500), DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        [Range(5, 120)]
        public int Runtime { get; set; }

        [Required, MaxLength(100), Display(Name = "Title")]
        public string Title { get; set; }

        [ForeignKey("Season")]
        public int SeasonId { get; set; }

        [ForeignKey("TvShowId")]
        public virtual Season Season { get; set; }

        [InverseProperty("History")]
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        [NotMapped]
        [Display(Name = "Épisode", ResourceType = typeof(StringEpisodes))]
        public string E00Format { get => $"E{this.Number.ToString("D2")}"; }

        [NotMapped]
        public string S00E00Format { get => $"{this.Season.ShortSeasonName}{this.E00Format}"; }

        [NotMapped]
        public string Image { get => $"{this.Season.BasePath}/{this.S00E00Format}.jpg"; }

        [NotMapped]
        public string Video { get => $"/Content/Media/video.mp4"; }
    }
}
