
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
 
        public class UsuariosController : Controller
        {
            public ApiServices apiServices;
            public Services.Services services;
            public HomeController hc;

            public UsuariosController()
            {
                apiServices = new ApiServices();
                services = new Services.Services();
                hc = new HomeController();
            }


            public ActionResult Index()
            {
                return View();
            }


            public object Get()
            {
                object o;

                try
                {
                    o = services.Get<Usuario>("usuarios").Select(

                        x => new
                        {
                            x.UsuarioIdInt,
                            x.NombreVar,
                            IsActiveBit =  x.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                        }

                        ).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return o;
            }


            public JsonResult List()
            {
                object o;

                try
                {
                    o = services.Get<Usuario>("usuarios").Select(

                        x => new
                        {
                            x.UsuarioIdInt,
                            x.NombreVar,
                            IsActiveBit = x.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                        }

                        ).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return Json(o, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            public JsonResult GetByID(int id)
            {
                object o;

                try
                {
                    o = services.Get<Usuario>("usuarios").Select(

                        x => new
                        {
                            x.UsuarioIdInt,
                            x.NombreVar,
                            FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                        }

                        ).Where(x => x.UsuarioIdInt == id).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return Json(o, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
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
                                     x => x.NombreVar == o.NombreVar
                                     ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + nameof(existe.NombreVar).ToString()
                                         + Environment.NewLine
                                         + "Verifique";

                            return Json(new { message });

                        }

                    // ADD
                    o.UsuarioIdInt = 1;



                    result = apiServices.Save<Usuario>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Add", o);

                    }

                    else
                    {

                        //UPDATE
                        var existe = services.Get<Usuario>("usuarios").
                                    Where(
                                    x => x.NombreVar == o.NombreVar
                                    &&
                                    x.UsuarioIdInt != o.UsuarioIdInt
                                    ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + nameof(existe.NombreVar).ToString()
                                         + Environment.NewLine
                                         + "Verifique";

                            return Json(new { message });

                        }


                        result = apiServices.Save<Usuario>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Update", o);


                    }

                }
                catch (Exception)
                {
                    return null;
                }

                if (result == null) return null;


                return Json(new { message = Get() });
            }

        } 
    }

 
   