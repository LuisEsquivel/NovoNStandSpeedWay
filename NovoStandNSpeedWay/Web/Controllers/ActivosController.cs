using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class ActivosController : Controller
    {

            public ApiServices apiServices;
            public Services.Services services;
            public HomeController hc;

            public ActivosController()
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
                    o = services.Get<Activo>("activos").Select(

                        x => new
                        {
                            x.ActivoIdInt ,
                            x.DescripcionVar,
                            FechaAlta = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
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
                    o = services.Get<Activo>("activos").Select(

                        x => new
                        {
                            x.UbicacionIdVar,
                            x.DescripcionVar,
                            FechaAlta = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
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
                    o = services.Get<Activo>("activos").Select(

                        x => new
                        {
                            x.ActivoIdInt,
                            x.DescripcionVar,
                            FechaAlta = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
                        }

                        ).Where(x => x.ActivoIdInt  == id).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return Json(o, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            public JsonResult Add(Activo o, string IsActive, string Accion)
            {
              
                Activo result = null;

                try
                {
                    //nuevo
                    if (Accion == null || Accion == "")
                    {

                        var existe = services.Get<Activo>("activos").
                                     Where(
                                     x => x.UbicacionIdVar == o.UbicacionIdVar
                                     ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + "Descripción"
                                         + Environment.NewLine
                                         + "Verifique";

                            return Json(new { message });

                        }

                        // ADD 
                        result = apiServices.Save<Activo>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.ActivosController, "Add", o);

                    }

                    else
                    {

                        //UPDATE
                        var existe = services.Get<Activo>("activos").
                                    Where(
                                    x => x.DescripcionVar == o.DescripcionVar
                                    &&
                                    x.ActivoIdInt  != o.ActivoIdInt 
                                    ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + "Id"
                                         + Environment.NewLine
                                         + "Verifique";

                            return Json(new { message });

                        }


                        //var NombreUsuario = "";
                        //if (hc.UserId() > 0)
                        //{
                        //    NombreUsuario = services.Get<Usuario>("usuario")
                        //                                         .Where
                        //                                         (x => x.UsuarioIdInt == hc.UserId()
                        //                                         ).FirstOrDefault().NombreVar;
                        //}


                        result = apiServices.Save<Activo>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.ActivosController, "Update", o);


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
