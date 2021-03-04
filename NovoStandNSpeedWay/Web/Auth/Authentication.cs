using NovoLeadsWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Auth
{
    public class Authentication : AuthorizeAttribute
    {

        public HomeController hc;
        public Authentication()
        {
            hc = new HomeController(); 
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.Cookies.Get(hc.CockieName) == null)
            {
               filterContext.Result = new RedirectResult("~/Login/Index");
            }

        }

    }
}