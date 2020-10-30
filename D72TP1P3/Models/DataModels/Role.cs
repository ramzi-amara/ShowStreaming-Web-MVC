namespace D72TP1P3.Models.DataModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Role {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None), ForeignKey("Actor")]
        public int ActorId { get; set; }

        [ForeignKey("ActorId"), InverseProperty("Roles")]
        public virtual Actor Actor { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None), ForeignKey("TvShow")]
        public int TvShowId { get; set; }

        [ForeignKey("TvShowId"), InverseProperty("Roles")]
        public virtual TvShow TvShow { get; set; }

        [MaxLength(50)]
        public string Character { get; set; }

    }
}
