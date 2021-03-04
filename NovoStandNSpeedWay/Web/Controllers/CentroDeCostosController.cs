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
    public class CentroDeCostosController : Controller
    {
        public ApiServices apiServices;
        public Services.Services services;
        public HomeController hc;
     

        public CentroDeCostosController()
        {
            apiServices = new ApiServices();
            services = new Services.Services();
            hc = new HomeController();
        }

        // GET: CentroCosto
        public ActionResult Index()
        {
            return View();
        }


        public object Get()
        {
            object list;

            try
            {
                var centroConstos = services.Get<CentroCosto>("centrodecostos");


                 list = (from c in centroConstos
                        select new
                        {
                            c.CentroCostosIdVar,
                            c.DescripcionVar,
                            ActivoBit = c.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(c.FechaAltaDate).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return list;
        }


        public JsonResult Listar()
        {
            object list;

            try
            {
                var centroConstos = services.Get<CentroCosto>("centrodecostos");

                list = (from c in centroConstos
                        select new
                        {
                            c.CentroCostosIdVar,
                            c.DescripcionVar,
                            ActivoBit = c.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(c.FechaAltaDate).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetByID(string id)
        {
            object o;

            try
            {
                var CentroDeCostos = services.Get<CentroCosto>("centrodecostos");
                o = (from c in CentroDeCostos
                     where c.CentroCostosIdVar == id
                     select new
                     {
                         c.CentroCostosIdVar,
                         c.DescripcionVar,
                         c.ActivoBit,

                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(CentroCosto o, string IsActive, string Accion)
        {
            CentroCosto result = null;
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            
            try
            {
                if (Accion == null || Accion == "") 
                {
                    var existe = services.Get<CentroCosto>("centrodecostos").
                    Where(x => x.CentroCostosIdVar == o.CentroCostosIdVar
                                 ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + nameof(existe.CentroCostosIdVar).ToString()
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    o.UsuarioIdInt = hc.UserId();

                    // ADD
                    result = apiServices.Save<CentroCosto>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.CentroDeCostosController, "Add", o);

                }

                else
                {

                    //UPDATE
                    var existe = services.Get<CentroCosto>("centrodecostos").
                                Where(
                                x => x.DescripcionVar == o.DescripcionVar  && 
                                     x.CentroCostosIdVar != o.CentroCostosIdVar).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + nameof(existe.CentroCostosIdVar).ToString()
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    o.UsuarioIdModInt = hc.UserId();
                    result = apiServices.Save<CentroCosto>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.CentroDeCostosController, "Update", o);


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