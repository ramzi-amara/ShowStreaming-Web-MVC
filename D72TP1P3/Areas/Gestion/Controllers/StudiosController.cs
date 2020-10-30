using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {

    [Authorize(Roles = "Administrator")]
    public class StudiosController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.Studios.ToList());
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Studio studio) {
            if (this.ModelState.IsValid) {
                this.db.Studios.Add(studio);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(studio);
        }

        public ActionResult Edit(int id) {
            Studio studio = this.db.Studios.Find(id);
            if (studio == null) {
                return this.HttpNotFound();
            }
            return this.View(studio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Studio studio) {
            if (this.ModelState.IsValid) {
                this.db.Entry(studio).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(studio);
        }

        public ActionResult Delete(int id) {
            Studio studio = this.db.Studios.Find(id);
            if (studio == null) {
                return this.HttpNotFound();
            }
            return this.View(studio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Studio studio = this.db.Studios.Find(id);
            this.db.Studios.Remove(studio);
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
