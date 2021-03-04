
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Web.CoreResources;
using Web.Models;
using Web.Services;

namespace NovoLeadsWeb.Controllers

{
    public class RegistrarseController : Controller
    {

        public ApiServices apiServices;
        public Services services;
        public HomeController hc;

        public RegistrarseController()
        {
            apiServices = new ApiServices();
            services = new Services();
            hc = new HomeController();
        }

        // GET: Registrarse
        public ActionResult Index()
        {
            Session["Registrarse"] = true;
            return View();
        }

        public ActionResult VerificarCuenta()
        {
            return View();
        }


        //olvidé la contraseña
        public ActionResult OlvideContraseña()
        {
            return View();
        }



        //Enviar Código De Verificación
        public JsonResult EnViarCodigoDeverificacion(string UsuarioVar)
        {

            object result = null;

            var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();

            if (usuario.UsuarioIdInt > 0)
            {
                var random = new Random();
                //usuario.CodigoVerificacion = random.Next(0, 999999).ToString();
                result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "Update", usuario);

                if (result != null)
                {
                    return Json(1);
                }
            }

            return Json(0);
        }



        public JsonResult Add(Usuario o, string IsActive)
        {
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            Usuario result = null;

            try
            {
                if (o.UsuarioIdInt == 0)
                {

                    var existe = services.Get<Usuario>("usuarios").
                                 Where(
                                 x => x.UsuarioVar == o.UsuarioVar
                                 ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + nameof(existe.UsuarioVar).ToString()
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    // ADD
                    o.FechaAltaDate = DateTime.Now;


                    result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController , "Add", o);

                }

            }
            catch (Exception)
            {
                return null;
            }

            if (result == null) return Json(0);




            HttpCookie cookie = new HttpCookie(hc.CockieName);
            cookie.Value = o.UsuarioVar;
            cookie.Expires = DateTime.Now.AddMonths(1);
            cookie.HttpOnly = true;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            return Json(1);
        }



        public JsonResult ValidarCuenta(string CodigoVerificacion)
        {

            object result = null;

            try
            {

              var UsuarioVar = System.Web.HttpContext.Current.Request.Cookies.Get(hc.CockieName).Value;
              var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();
              
              //if(CodigoVerificacion == usuario.)
              //  {
              //      var c = new HttpCookie(hc.CockieName);
              //      c.Expires = DateTime.Now.AddDays(-1);
              //      Response.Cookies.Add(c); ;
              //      return Json(1);
              //  }

              result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "Add", usuario);

            }
            catch (Exception)
            {
                return null;
            }

            return Json(0);

        }

    }


}