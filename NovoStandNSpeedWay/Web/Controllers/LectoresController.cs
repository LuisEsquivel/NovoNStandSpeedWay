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
    public class LectoresController : Controller
    {
        public ApiServices apiServices;
        public Services.Services services;
        public HomeController hc;

        public LectoresController()
        {
            apiServices = new ApiServices();
            services = new Services.Services();
            hc = new HomeController();
        }

        // GET: Lectores
        public ActionResult Index()
        {
            return View();
        }


        public object Get()
        {
            object list;

            try
            {
                var lectores = services.Get<Lectore>("lectores");

                list = (from l in lectores
                        select new
                        {
                            l.LectorIdInt,
                            l.DescripcionVar,
                            l.DireccionVar,
                            l.ModeloVar,
                            FechaAlta = Convert.ToDateTime(l.FechaAltaDate).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Listar()
        {
            object list;

            try
            {
                var lectores = services.Get<Lectore>("lectores");

                list = (from l in lectores
                        select new
                        {
                            l.LectorIdInt,
                            l.DescripcionVar,
                            l.DireccionVar,
                            l.ModeloVar,
                            FechaAlta = Convert.ToDateTime(l.FechaAltaDate).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetByID(int id)
        {
            object o;

            try
            {
                var lectores = services.Get<Lectore>("lectores");
                o = (from l in lectores
                     where l.LectorIdInt == id
                     select new
                     {
                         l.LectorIdInt,
                         l.DescripcionVar,
                         l.DireccionVar,
                         l.ModeloVar
                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(Lectore o)
        {
            Lectore result = null;



            try
            {
                if (o.LectorIdInt == 0)
                {

                    var existe = services.Get<Lectore>("lectores").
                                 Where(
                                 x => x.ModeloVar == o.ModeloVar
                                 ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + nameof(existe.ModeloVar).ToString()
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    o.UsuarioIdInt = hc.UserId(); ;

                    // ADD
                    result = apiServices.Save<Lectore>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.LectoresController, "Add", o);

                }

                else
                {

                    //UPDATE
                    var existe = services.Get<Lectore>("lectores").
                                Where(
                                x => x.ModeloVar == o.ModeloVar
                                ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + nameof(existe.ModeloVar).ToString()
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    o.UsuarioIdModInt = hc.UserId();
                    result = apiServices.Save<Lectore>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.LectoresController, "Update", o);


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