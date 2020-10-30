namespace D72TP1P3.Areas.Gestion.Controllers {
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using D72TP1P3.Models.DataModels;

    [Authorize(Roles = "Administrator")]
    public class SeasonsController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.TvShows.ToList());
        }

        public ActionResult Create() {
            this.ViewBag.TvShowId = new SelectList(this.db.TvShows, "TvShowId", "Title");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Season season) {
            if (this.ModelState.IsValid) {
                this.db.Seasons.Add(season);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.TvShowId = new SelectList(this.db.TvShows, "TvShowId", "Title", season.TvShowId);
            return this.View(season);
        }

        public ActionResult Edit(int id) {
            Season season = this.db.Seasons.Find(id);
            if (season == null) {
                return this.HttpNotFound();
            }
            this.ViewBag.TvShowId = new SelectList(this.db.TvShows, "TvShowId", "Title", season.TvShowId);
            return this.View(season);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Season season) {
            if (this.ModelState.IsValid) {
                this.db.Entry(season).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            this.ViewBag.TvShowId = new SelectList(this.db.TvShows, "TvShowId", "Title", season.TvShowId);
            return this.View(season);
        }


        public ActionResult Delete(int id) {
            Season season = this.db.Seasons.Find(id);
            if (season == null) {
                return this.HttpNotFound();
            }
            return this.View(season);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Season season = this.db.Seasons.Find(id);
            this.db.Seasons.Remove(season);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
