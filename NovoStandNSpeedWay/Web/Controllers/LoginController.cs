

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
        public Services services;
        public Generals g;


        public LoginController()
        {
            apiServices = new ApiServices();
            services = new Services();
            g = new Generals();
        }
        // GET: Login


        public ActionResult Index()
        {

            if (g.GetCookieValue(g.CockieName) != null )
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
            if (g.GetCookieValue(g.CockieName) != null)
            {
                var c = new HttpCookie(g.CockieName);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c); ;
            }

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
                        if (g.SendEmailSMTP(user.UsuarioVar, user.CodigoDeVerificacionVar))
                        {
                            g.CreateCookie(user.UsuarioVar);
                            return Json("/Registrarse/VerificarCuenta");
                        }
                    }


                    var value = user.UsuarioIdInt.ToString();
                    g.CreateCookie(value);
                    return Json(user.NombreVar);

                }

                      
             }



                   return Json(0);

           }





    }


}