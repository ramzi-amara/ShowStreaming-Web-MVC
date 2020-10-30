using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace D72TP1P3 {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override void Init()
        {
            this.BeginRequest += GlobalBeginRequest;
        }
        protected void GlobalBeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["LanguageCookie"];
            if (cookie != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
               new CultureInfo(cookie.Values["CurrentLanguage"]);
                System.Threading.Thread.CurrentThread.CurrentUICulture =
               new CultureInfo(cookie.Values["CurrentLanguage"]);
            }
        }
    }
}
