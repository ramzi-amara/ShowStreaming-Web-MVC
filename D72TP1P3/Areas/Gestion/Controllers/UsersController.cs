using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {

   // [Authorize(Roles = "Administrator")]
    public class UsersController : Controller {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            return this.View(this.db.Users.ToList());
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user) {
            if (this.ModelState.IsValid) {
                this.db.Users.Add(user);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(user);
        }

        public ActionResult Edit(int id) {
            User user = this.db.Users.Find(id);
            if (user == null) {
                return this.HttpNotFound();
            }
            return this.View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user) {
            if (this.ModelState.IsValid) {
                this.db.Entry(user).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(user);
        }

        public ActionResult Delete(int id) {
            User user = this.db.Users.Find(id);
            if (user == null) {
                return this.HttpNotFound();
            }
            return this.View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            User user = this.db.Users.Find(id);
            this.db.Users.Remove(user);
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
