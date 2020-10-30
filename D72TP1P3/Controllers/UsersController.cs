using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using D72TP1P3.Models.DataModels;
using D72TP1P3.Models.ViewModels.User;

namespace D72TP1P3.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Edit");
            }
            return this.RedirectToAction("SignUp");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            return this.View(new SignUp());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult SignUp(SignUp s)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    User u = new User();
                    u.Email = s.Email;
                    u.Password = s.Password;
                    u.UserName = s.UserName;
                    this.db.Users.Add(u);
                    this.db.SaveChanges();
                    return this.RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    while (e.InnerException != null) { e = e.InnerException; }
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(s);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return this.View(new Login());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(Login login)
        {
            if (this.ModelState.IsValid)
            {
                //Envoyer le cookie d'authentification lorsque Login valide
                //Dans cet exemple on envoi le UserId
                User user = this.db.Users.Single(u => u.UserName == login.UserName);
                FormsAuthentication.SetAuthCookie(user.UserId.ToString(), login.RememberMe);
                if (user.Type == D72TP1P3.Models.DataModels.User.UserType.Administrator)
                {
                    return RedirectToAction("Index", "Home", new { area = "Gestion" });
                }
                else
                    return this.RedirectToAction("Index", "Library");
            }
            else
            {
                return this.View(login);
            }
        }

        [HttpGet, Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Edit()
        {
            //Obtenir les données de l'User courant
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            //L'User possède un cookie d'authentification mais l'User n'existe plus, faire un logout.
            if (user == null) { return this.RedirectToAction("Logout", "User"); }

            //Placer les données de l'User courant dans les données de la vue
            Profile p = new Profile();
            p.Email = user.Email;

            //Envoyer la vue à l'User
            return this.View(p);
        }

        [HttpPost, Authorize]
        public ActionResult Edit(Profile p)
        {
            //Si la vue contient des données valide, procéder aux modifications demandées par l'User
            if (this.ModelState.IsValid)
            {
                try
                {
                    //Obtenir les données de l'User courant
                    User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));

                    //Le Email a peut-être été modifié, et est obligatoire
                    user.Email = p.Email;

                    //Si l'User a spécifié un nouveau mot de passe, remplacer l'ancien
                    if (!string.IsNullOrEmpty(p.NewPassword))
                    {
                        user.Password = p.NewPassword;
                    }

                    //Lancer UPDATE ds la BD 
                    this.db.Entry(user).State = EntityState.Modified;
                    this.db.SaveChanges();
                    return this.RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    while (e.InnerException != null) { e = e.InnerException; }
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(p);
        }

        [HttpGet, Authorize]
        public ActionResult Delete()
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            //L'User possède un cookie d'authentification mais l'User n'existe plus, faire un logout.
            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            return this.View(user);
        }

        [HttpPost, Authorize]
        public ActionResult Delete(User ignore)
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));

            if (user.UserName == "admin")
            {
                return this.Content(@"<h1 class=""text-danger"">Le compte admin ne peut pas être supprimé.</h1>");
            }
            try
            {
                user.History.Clear();
                user.Favorites.Clear();
                this.db.Users.Remove(user);
                this.db.SaveChanges();
                FormsAuthentication.SignOut();
                return this.RedirectToAction("Index", "Library");
            }
            catch (Exception e)
            {
                while (e.InnerException != null) { e = e.InnerException; }
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult History(int? EpisodeId = null)
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            return this.View(user.History);

        }


        [Authorize]
        public ActionResult ListFavorite()
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            return this.View(user.Favorites.ToList());
        }

        [Authorize]
        public ActionResult Favorite(int? IdTvShow = null)
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            TvShow tv = this.db.TvShows.Find(IdTvShow);
            user.Favorites.Add(tv);
            this.db.SaveChanges();
            return this.RedirectToAction("ListFavorite", "Users");
        }

        [Authorize]
        public ActionResult DeleteFavorite(int? IdTvShow = null) {
            try
            {
                User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
                if (user == null) { return this.RedirectToAction("Logout", "User"); }
                TvShow tv = this.db.TvShows.Find(IdTvShow);
                user.Favorites.Remove(tv);
                this.db.SaveChanges();
                return this.RedirectToAction("ListFavorite", "Users");
            }
            catch (Exception e) {
                ModelState.Clear();
                ModelState.AddModelError("Erreur", e.Message);
            }
            return this.View();
            
        }


    }
}
