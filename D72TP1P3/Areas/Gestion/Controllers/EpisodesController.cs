using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {
    [Authorize(Roles = "Administrator")]

    public class EpisodesController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.TvShows.ToList());
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Episode episode) {
            if (this.ModelState.IsValid) {
                this.db.Episodes.Add(episode);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.SeasonId = new SelectList(this.db.Seasons, "SeasonId", "Number", episode.SeasonId);
            return this.View(episode);
        }

        public ActionResult Edit(int id) {
            Episode episode = this.db.Episodes.Find(id);
            if (episode == null) {
                return this.HttpNotFound();
            }
            return this.View(episode);
        }       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Episode episode) {
            if (this.ModelState.IsValid) {
                this.db.Entry(episode).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(episode);
        }
        
        public ActionResult Delete(int id) {
            Episode episode = this.db.Episodes.Find(id);
            if (episode == null) {
                return this.HttpNotFound();
            }
            return this.View(episode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Episode episode = this.db.Episodes.Find(id);
            this.db.Episodes.Remove(episode);
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
