using NovoLeadsWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Web.Helpers;
using Web.Models;

namespace Web.Auth
{
    public class Authentication : AuthorizeAttribute
    {

        public HomeController hc;
        public Generals g;
        public Services.Services services;
        public Authentication()
        {
            hc = new HomeController();
            services = new Services.Services();
            g = new Generals();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (g.GetCookieValue(g.CockieName) == null)
            {
               filterContext.Result = new RedirectResult("~/Login/Index");
            }
            else
            {
           
                var UsuarioIdInt = g.GetCookieValue(g.CockieName);

                var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioIdInt.ToString() == UsuarioIdInt).FirstOrDefault();

                if (usuario == null) filterContext.Result = new RedirectResult("~/Login/Index");

            }


   

        }

    }
}