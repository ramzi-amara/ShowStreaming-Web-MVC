namespace D72TP1P3.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.UI;
    using D72TP1P3.Models.DataModels;

    public class LibraryController : Controller
    {
        TVShowDb tdb = new TVShowDb();
        private const int NbEpisode = 12;

        [HttpGet]
        public ActionResult Index(string FiltreTitle = "", int? FiltreGenre = null, int? FiltreStudio = null)
        {
            IQueryable<TvShow> ltv = tdb.TvShows;
            if (FiltreTitle != "")
            {
                ltv = ltv.Where(model => model.Title.ToLower().Contains(FiltreTitle.ToLower()));
            }

            if (FiltreGenre != null)
            {
                ltv = ltv.Where(model => model.Genres.Any(tv => tv.GenreId == FiltreGenre.Value));
            }
            if (FiltreStudio != null)
            {
                ltv = ltv.Where(model => model.StudioId == FiltreStudio);
            }
            this.ViewBag.FiltreTitle = FiltreTitle;
            this.ViewBag.FiltreGenre = FiltreGenre;
            this.ViewBag.FiltreStudio = FiltreStudio;
            return this.View(ltv.ToList());
        }

        public ActionResult TvShow(int? IdTvShow = null)
        {
            TvShow tv = tdb.TvShows.Find(IdTvShow);
            return this.View(tv);
        }

        public ActionResult Season(int SaisonId, int Page=1)
        {
            ViewBag.TotalPages = (int)Math.Ceiling((double)this.tdb.Episodes.Where(model=>model.SeasonId==SaisonId).Count() / NbEpisode);
            ViewBag.Page = Page = Math.Max(Page, 1);
            ViewBag.SaisonId = SaisonId;
            ViewBag.Page = Page = Math.Min(Page, (int)ViewBag.TotalPages);
            Season s = tdb.Seasons.Find(SaisonId);
            return View(s.Episodes.OrderBy(a=>a.EpisodeId).Skip(NbEpisode * (Page - 1)).Take(NbEpisode).ToList());
        }

        public ActionResult Episode(int? EpisodeId = null)
        {
            Episode ep = tdb.Episodes.Find(EpisodeId);

            return View(ep);
        }
        public ActionResult ViewEpisode(int EpisodeId)
        {
            User user = this.tdb.Users.Find(int.Parse(this.User.Identity.Name));
            if (user == null) { return this.RedirectToAction("Logout", "Users"); }
            Episode ep = this.tdb.Episodes.Find(EpisodeId);
            if (user.History.Any(e => e.EpisodeId == ep.EpisodeId))
            {
                user.History.Add(ep);
                this.tdb.SaveChanges();
            }
            return View(ep);
        }
    }
}