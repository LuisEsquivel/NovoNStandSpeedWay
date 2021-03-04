using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Auth;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{

    [Authentication]
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
                            x.EstadoActivoVar,
                            FechaAdquisicion = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
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
                            x.ActivoIdInt,
                            x.DescripcionVar,
                            x.EstadoActivoVar,
                            FechaAdquisicion = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
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
                o = services.Get<Activo>("activos").Select

                (
                     x => new
                     {
                         x.ActivoIdInt,
                         x.CentroCostosIdVar,
                         x.EdificioVar,
                         x.NoSerieVar,
                         x.BarcodeVar,
                         x.UbicacionIdVar,
                         x.EstadoActivoVar,
                         x.FormaAdquisicionIdInt,
                         FechaAdquisicionDate = x.FechaAdquisicionDate.ToString("yyyy-MM-dd"),
                         x.DocumentoVar,
                         x.CostoDec,
                         x.EpcVar,
                         x.DescripcionVar,
                         x.IdentificadorActivoVar,
                         x.MarcaVar,
                         x.ModeloVar,
                         x.PisoVar,
                         x.DepAcumuladaVar,
                         x.ValorenLibrosVar,
                         x.AdicionalVar,
                         x.Adicional2Var,
                         x.Adicional3Var,
                         x.Adicional4Var,
                         x.Adicional5Var,
                         x.Adicional6Var,
                         x.Adicional7Var,
                         x.Adicional8Var,
                         x.Adicional9Var,
                         x.Adicional10Var,
                         x.Adicional11Dec,
                         x.Adicional12Dec,
                         x.Adicional13Dec,
                         x.Adicional14Dec,
                         Adicional15Date = x.Adicional15Date.ToString("yyyy-MM-dd"),
                         Adicional16Date = x.Adicional16Date.ToString("yyyy-MM-dd"),
                     }
                    ).ToList().Where(a => a.ActivoIdInt  == id);
                }
                catch (Exception)
                {
                    return null;
                }

                
                return Json(o, JsonRequestBehavior.AllowGet);
            }


            
            [HttpPost]
            public JsonResult Add(Activo o, string IsActive)
            {
              
                Activo result = null;

                try
                {
                    //nuevo
                    if (o.ActivoIdInt == 0)
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
