namespace D72TP1P3.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DisplayColumn(displayColumn: "Number")]
    public class Season {
        [Key]
        public int SeasonId { get; set; }

        [Range(1, 25)]
        public byte Number { get; set; }

        [ForeignKey("TvShow")]
        public int TvShowId { get; set; }

        [ForeignKey("TvShowId")]
        public virtual TvShow TvShow { get; set; }

        [InverseProperty("Season")]
        public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();

        [NotMapped]
        public string TVShowAndSeasonName { get => $"{this.TvShow.Title} - S{this.Number.ToString("D2")}"; }

        [NotMapped]
        public string ShortSeasonName { get => $"S{this.Number.ToString("D2")}"; }

        [NotMapped]
        public string BasePath { get => $"{this.TvShow.BasePath}/{this.ShortSeasonName}"; }

        [NotMapped]
        public string Image { get => $"{this.BasePath}/{this.ShortSeasonName}.jpg"; }
    }
}
