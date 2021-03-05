using NovoLeadsWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Web.Models;

namespace Web.Auth
{
    public class Authentication : AuthorizeAttribute
    {

        public HomeController hc;
        public Services.Services services;
        public Authentication()
        {
            hc = new HomeController();
            services = new Services.Services();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (System.Web.HttpContext.Current.Request.Cookies.Get(hc.CockieName) == null)
            {
               filterContext.Result = new RedirectResult("~/Login/Index");
            }
            else
            {
           
                var UsuarioIdInt = System.Web.HttpContext.Current.Request.Cookies.Get(hc.CockieName).Value.ToString();

                var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioIdInt.ToString() == UsuarioIdInt).FirstOrDefault();

                if (usuario == null) filterContext.Result = new RedirectResult("~/Login/Index");

            }


   

        }

    }
}