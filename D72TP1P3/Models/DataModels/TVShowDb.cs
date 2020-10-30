namespace D72TP1P3.Models.DataModels {
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class TVShowDb : DbContext {
        public TVShowDb() : base("TVShowDbConnectionString") {
            Database.SetInitializer(new TvShowDbInitializer());
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Season>().HasIndex(s => new {s.TvShowId , s.Number }).IsUnique();
            modelBuilder.Entity<Episode>().HasIndex(e => new { e.SeasonId, e.Number }).IsUnique();
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            modelBuilder.Entity<Studio>().HasMany(e => e.TvShows).WithRequired(e => e.Studio).WillCascadeOnDelete(false);
        }
    }
}
