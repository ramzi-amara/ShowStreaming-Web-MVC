namespace D72TP1P3.Models.DataModels {
    using System.Data.Entity;
    public class TvShowDbInitializer : DropCreateDatabaseIfModelChanges<TVShowDb> {
        protected override void Seed(TVShowDb context) {
            XML.Deserialize.DeserializeTVShow();

            context.Users.Add(new User {
                UserName = "admin",
                Password = "admin",
                Email = "admin@site.com",
                Type = User.UserType.Administrator,
            });
            context.SaveChanges();
        }
    }
}
