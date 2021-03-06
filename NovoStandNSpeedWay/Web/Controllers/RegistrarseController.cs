﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Web.CoreResources;
using Web.Helpers;
using Web.Models;
using Web.Services;

namespace NovoLeadsWeb.Controllers

{
    public class RegistrarseController : Controller
    {

        public ApiServices apiServices;
        public Services services;
        public HomeController hc;
        public Generals g;

        public RegistrarseController()
        {
            apiServices = new ApiServices();
            services = new Services();
            hc = new HomeController();
            g = new Generals();
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


        //actualizar la actualizar
        public ActionResult ActualizarContraseña()
        {
            return View();
        }



        //Enviar Código De Verificación
        public JsonResult EnviarCodigoDeverificacion(string UsuarioVar)
        {

            object result = null;

            var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();
            if (usuario == null)
            {
                return Json("No se encontró cuenta de Novo asociada a este correo: " + UsuarioVar + Environment.NewLine + "Verifica que el correo está bien escrito y vuelve a intentarlo");
            }

            if (usuario.UsuarioIdInt > 0)
            {
                var random = new Random();
                usuario.CodigoDeVerificacionVar = random.Next(0, 999999).ToString();
                usuario.Password = "XdXd";

                if(g.SendEmailSMTP(usuario.UsuarioVar, usuario.CodigoDeVerificacionVar) == false)
                {
                    return Json(0);
                }

                result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "Update", usuario);

                if (result != null)
                {
                    g.CreateCookie(UsuarioVar);
                    return Json(1);
                }
            }

            return Json(0);
        }




        public JsonResult UpdatePassword(Usuario o, string ConfirmPassword)
        {

            if (o == null) return Json(-1);

            if (o.Password != ConfirmPassword) return Json(0);

            var UsuarioVar = g.GetCookieValue(g.CockieName);
            var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();
            usuario.Password = o.Password;


            if (o.CodigoDeVerificacionVar != usuario.CodigoDeVerificacionVar) return (Json(2));

            var result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "Update", usuario);

            if (result == null) return Json(-1);



            g.CreateCookie(result.UsuarioIdInt.ToString());
            return Json(1);
            
        }



        public JsonResult Add(Usuario o, string IsActive = "true", bool GoogleAccount = false)
        {
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            Usuario result = null;

          
            // es cuenta de google
            if (GoogleAccount)
            {

                result = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == o.UsuarioVar).FirstOrDefault();
                
                if(result == null)
                {

                    o.ActivoBit = true;
                    
                    //agregamos el email a la BD
                    result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "Add", o);


                    //validamos la cuenta
                    o.UsuarioIdInt = result.UsuarioIdInt;
                    o.CuentaVerificadaBit = true;
                    result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "ValidateAccount", o);

                }


                if (result != null)
                {
                    g.CreateCookie(result.UsuarioIdInt.ToString());
                    return Json(1);
                }
               

                return Json(0);
            }



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
                                     + o.UsuarioVar
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    // ADD
                    o.FechaAltaDate = DateTime.Now;

                    var random = new Random();
                    o.CodigoDeVerificacionVar = random.Next(0, 999999).ToString();

                    if (g.SendEmailSMTP(o.UsuarioVar, o.CodigoDeVerificacionVar) == false)
                    {
                        return Json(0);
                    }

                    result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController , "Add", o);

                }

            }
            catch (Exception)
            {
                return Json(0);
            }

            if (result == null) return Json(0);


            //for verified account
            g.CreateCookie(o.UsuarioVar);
            return Json(1);
        }



        public JsonResult ValidarCuenta(string CodigoVerificacionVar)
        {

            object result = null;

            try
            {

              var UsuarioVar = g.GetCookieValue(g.CockieName);
              var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();

                if (CodigoVerificacionVar == usuario.CodigoDeVerificacionVar)
                {

                    usuario.CuentaVerificadaBit = true;
                    result = apiServices.Save<Usuario>(CoreResources.UrlBase, CoreResources.Prefix, CoreResources.UsuariosController, "ValidateAccount", usuario);

                    if (result != null) {
                        g.CreateCookie(usuario.UsuarioIdInt.ToString());
                        return Json(1);
                    }
                   

                }

            }
            catch (Exception)
            {
                return null;
            }

            return Json(0);

        }

    }


}