

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Controllers;
using Web.Helpers;
using Web.Models;
using Web.Services;




namespace NovoLeadsWeb.Controllers
{

    public class LoginController : Controller
    {

        public ApiServices apiServices;
        public HomeController hc;
        public Services services;
        public Generals generals;


        public LoginController()
        {
            apiServices = new ApiServices();
            hc = new HomeController();
            services = new Services();
            generals = new Generals();
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
  
                if (user.ActivoBit)
                {

                    //account not verified
                    if (user.CuentaVerificadaBit == false)
                    {

                        //send email code verification
                        var random = new Random();
                        user.CodigoDeVerificacionVar = random.Next(0, 999999).ToString();
                        if (generals.SendEmailSMTP(user.UsuarioVar, user.CodigoDeVerificacionVar))
                        {
                            hc.CreateCookie(user.UsuarioVar);
                            return Json("/Registrarse/VerificarCuenta");
                        }
                    }


                    var value = user.UsuarioIdInt.ToString();
                    hc.CreateCookie(value);
                    return Json(user.NombreVar);

                }

                      
             }



                   return Json(0);

           }





    }


}