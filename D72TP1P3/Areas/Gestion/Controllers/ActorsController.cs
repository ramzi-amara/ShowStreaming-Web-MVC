using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {

    [Authorize(Roles = "Administrator")]
    public class ActorsController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.Actors.ToList());
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor) {
            if (this.ModelState.IsValid) {
                this.db.Actors.Add(actor);
                this.db.SaveChanges();
                return this.RedirectToAction(actionName: "Index", controllerName: "Actors", routeValues: new { area = "Gestion" });
            }

            return this.View(actor);
        }

        public ActionResult Edit(int id) {
            Actor actor = this.db.Actors.Find(id);
            if (actor == null) {
                return this.HttpNotFound();
            }
            return this.View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actor actor) {
            if (this.ModelState.IsValid) {
                this.db.Entry(actor).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction(actionName: "Index", controllerName: "Actors", routeValues: new { area = "Gestion" });
            }
            return this.View(actor);
        }

        public ActionResult Delete(int id) {
            Actor actor = this.db.Actors.Find(id);
            if (actor == null) {
                return this.HttpNotFound();
            }
            return this.View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Actor actor = this.db.Actors.Find(id);
            this.db.Actors.Remove(actor);
            this.db.SaveChanges();
            return this.RedirectToAction(actionName: "Index", controllerName: "Actors", routeValues: new { area = "Gestion" });
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
