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
    public class HomeController : Controller
    {


        public ApiServices apiServices;
        public Services.Services services;


        public string CockieName = "UserIdNovoSpeedWay";


        public HomeController()
        {
            apiServices = new ApiServices();
            services = new Services.Services();
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public int UserId()
        {
            int userId = 0;

            if (System.Web.HttpContext.Current.Request.Cookies.Get(CockieName) != null)
            {
                userId = int.Parse(System.Web.HttpContext.Current.Request.Cookies.Get(CockieName).Value);
            }

            return userId;
        }



        

        #region DROPDOWNS

        public JsonResult DropDownUbicacion()
        {
            object list;
            try
            {
                list = services.Get<Ubicacione>("ubicaciones").Where(p => p.ActivoBit == true).Select(
                    x => new
                    {
                        IID = x.UbicacionIdVar,
                        NOMBRE = x.DescripcionVar
                    }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DropDownCentroDeCostos()
        {
            object list;
            try
            {
                list = services.Get<CentroCosto>("centrodecostos").Where(p => p.ActivoBit == true).Select(
                    x => new
                    {
                        IID = x.CentroCostosIdVar,
                        NOMBRE = x.DescripcionVar,
                        x.ActivoBit 
                    })
                    .Where(x => x.ActivoBit == true)
                    .OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DropDownFormaAdquisicion()
        {
            object list;
            try
            {
                list = services.Get<FormaAdquisicion>("formaadquisicion").Where(p => p.ActivoBit == true).Select(
                    x => new
                    {
                        IID = x.FormaAdquisicionIdInt,
                        NOMBRE = x.DescripcionVar,
                        x.ActivoBit 
                    })
                    .Where(x => x.ActivoBit == true)
                    .OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


       
        #endregion
    }
}