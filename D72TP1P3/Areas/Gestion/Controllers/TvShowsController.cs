using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D72TP1P3.Models.DataModels;

namespace D72TP1P3.Areas.Gestion.Controllers {

    [Authorize(Roles = "Administrator")]
    public class TvShowsController : Controller {
        private readonly TVShowDb db = new TVShowDb();
        private const int NbTvShowPerPage = 8;

        public ActionResult Index(int Page = 1) {
            //Calculer TotalPages
            ViewBag.TotalPages = (int)Math.Ceiling((double)this.db.TvShows.Count() / NbTvShowPerPage);
            //S'assurer que Page est >= 1
            ViewBag.Page = Page = Math.Max(Page, 1);
            //S'assurer que Page est <= TotalPages
            ViewBag.Page = Page = Math.Min(Page, (int)ViewBag.TotalPages);
            return this.View(this.db.TvShows
                .OrderBy(a => a.Title)
                .Skip(NbTvShowPerPage * (Page - 1))
                .Take(NbTvShowPerPage).ToList()
            );
        }

        public ActionResult Create() {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TvShow tvShow) {
            if (this.ModelState.IsValid) {
                this.db.TvShows.Add(tvShow);
                this.db.SaveChanges();
                return this.RedirectToAction(actionName: "Index", controllerName: "TvShows", routeValues: new { area = "Gestion" });
            }

            this.ViewBag.StudioId = new SelectList(this.db.Studios, "StudioId", "Name", tvShow.StudioId);
            return this.View(tvShow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TvShow tvShow) {
            if (this.ModelState.IsValid) {
                this.db.Entry(tvShow).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            this.ViewBag.StudioId = new SelectList(this.db.Studios, "StudioId", "Name", tvShow.StudioId);
            return this.View(tvShow);
        }

        public ActionResult Delete(int id) {
            TvShow tvShow = this.db.TvShows.Find(id);
            if (tvShow == null) {
                return this.HttpNotFound();
            }
            return this.View(tvShow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            TvShow tvShow = this.db.TvShows.Find(id);
            this.db.TvShows.Remove(tvShow);
            this.db.SaveChanges();
            return this.RedirectToAction(actionName: "Index", controllerName: "TvShows", routeValues: new { area = "Gestion" });
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
