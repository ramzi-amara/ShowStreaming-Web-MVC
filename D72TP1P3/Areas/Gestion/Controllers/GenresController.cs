using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {
    [Authorize(Roles = "Administrator")]

    public class GenresController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.Genres.ToList());
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre) {
            if (this.ModelState.IsValid) {
                this.db.Genres.Add(genre);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(genre);
        }

        public ActionResult Edit(int id) {
            Genre genre = this.db.Genres.Find(id);
            if (genre == null) {
                return this.HttpNotFound();
            }
            return this.View(genre);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre) {
            if (this.ModelState.IsValid) {
                this.db.Entry(genre).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(genre);
        }

        
        public ActionResult Delete(int id) {
            Genre genre = this.db.Genres.Find(id);
            if (genre == null) {
                return this.HttpNotFound();
            }
            return this.View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Genre genre = this.db.Genres.Find(id);
            this.db.Genres.Remove(genre);
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
