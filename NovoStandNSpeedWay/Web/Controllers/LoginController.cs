

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Controllers;
using Web.Models;
using Web.Services;

namespace NovoLeadsWeb.Controllers
{

    public class LoginController : Controller
    {

        public ApiServices apiServices;
        public HomeController hc;
        public Services services;

        public LoginController()
        {
            apiServices = new ApiServices();
            hc = new HomeController();
            services = new Services();
        }
        // GET: Login


        public ActionResult Index()
        {
            Session["Registrarse"] = null;

            if (System.Web.HttpContext.Current.Request.Cookies[hc.CockieName] != null )
            {
                return Redirect("/Home/Index");
            }
            else
            {
                    return View();
            }
            
        }


        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.Request.Cookies[hc.CockieName] != null)
            {
                var c = new HttpCookie(hc.CockieName);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c); ;
            }

            //Session.Remove("userId");
            Session.Remove("Registrarse");
            return Redirect("../Login/Index");
        }


        public JsonResult Access(Usuario user)
        {
            var res = services.Login<Usuario>(user);

            if (res != null)
            {
                user = (Usuario)res;
                var value = user.UsuarioIdInt.ToString();

                if (user.ActivoBit)
                {
                HttpCookie cookie = new HttpCookie(hc.CockieName);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddMonths(1);
                cookie.HttpOnly = true;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                return Json(1);
                }

            }


            return Json(0);
        }





    }
}