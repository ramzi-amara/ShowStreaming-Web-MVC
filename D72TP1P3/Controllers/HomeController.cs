using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace D72TP1P3.Controllers {
    using D72TP1P3.Models.DataModels;

    public class HomeController : Controller {
        public ActionResult Index() {
            return this.View();
        }
        public ActionResult ChangeLanguage(string lang = "en")
        {
            HttpCookie cookie = this.Response.Cookies["LanguageCookie"];
            if (cookie == null)
            {
                cookie = new HttpCookie("LanguageCookie");
            }
            else
            {   
                Response.Cookies.Remove("LanguageCookie");
            }
            cookie.Values["CurrentLanguage"] = lang;
            cookie.Expires = DateTime.UtcNow.AddYears(1);
            Response.AppendCookie(cookie);
            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}