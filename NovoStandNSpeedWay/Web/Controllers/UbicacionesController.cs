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
    public class UbicacionesController : Controller
    {
        public ApiServices apiServices;
        public Services.Services services;
        public HomeController hc;

        public UbicacionesController()
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
                o = services.Get<Ubicacione>("ubicaciones").Select(

                    x => new
                    {
                        x.UbicacionIdVar,
                        x.DescripcionVar,
                        IsActiveBit = x.ActivoBit != false ? "SI" : "NO",
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
                o = services.Get<Ubicacione>("ubicaciones").Select(

                    x => new
                    {
                        x.UbicacionIdVar,
                        x.DescripcionVar,
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
        public JsonResult GetByID(string id)
        {
            object o;

            try
            {
                o = services.Get<Ubicacione>("ubicaciones").Select(

                    x => new
                    {
                        x.UbicacionIdVar,
                        x.DescripcionVar,
                        x.ActivoBit,
                        FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                    }

                    ).Where(x => x.UbicacionIdVar == id).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(Ubicacione o, string IsActive, string Accion)
        {
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            Ubicacione result = null;
            

            try
            {
                //nuevo
                if (Accion == null || Accion == "")
                {

                    var existe = services.Get<Ubicacione>("ubicaciones").
                                 Where(
                                 x => x.UbicacionIdVar  == o.UbicacionIdVar
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

                    // ADD
                    o.UsuarioIdInt = hc.UserId();
                    result = apiServices.Save<Ubicacione>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UbicacionesController, "Add", o);

                }

                else
                {

                    //UPDATE
                    var existe = services.Get<Ubicacione>("ubicaciones").
                                Where(
                                x => x.DescripcionVar == o.DescripcionVar
                                &&
                                x.UbicacionIdVar  != o.UbicacionIdVar
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


                    o.UsuarioIdModInt = hc.UserId();
                    result = apiServices.Save<Ubicacione>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UbicacionesController, "Update", o);


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