namespace D72TP1P3.Models.DataModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DisplayColumn("Name")]
    public class Studio {
        [Key]
        public int StudioId { get; set; }

        [Required, MaxLength(50), Display(Name= "Studio")]
        public string Name { get; set; }

        [InverseProperty("Studio")]
        public virtual ICollection<TvShow> TvShows { get; set; } = new HashSet<TvShow>();

    }
}
