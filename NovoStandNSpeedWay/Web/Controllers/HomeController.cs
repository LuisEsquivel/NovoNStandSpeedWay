using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";

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



        //crete cookie for add value in browser
        public void CreateCookie(string value)
        {

            if (System.Web.HttpContext.Current.Request.Cookies[CockieName] != null)
            {
               if(System.Web.HttpContext.Current.Request.Cookies[CockieName].Value.ToString().Trim().Length > 0){
                    var c = new HttpCookie(CockieName);
                    c.Expires = DateTime.Now.AddDays(-1);
                    System.Web.HttpContext.Current.Response.Cookies.Add(c); 
                }
                
        }

  

            HttpCookie cookie = new HttpCookie(CockieName);
            cookie.Value = Encrypt(value); 
            cookie.Expires = DateTime.Now.AddMonths(1);
            cookie.HttpOnly = true;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }




        public string Encrypt(string value)
        {

            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            //Algoritmo 3DAS
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la cadena cifrada
            var BytesValue = Encoding.UTF8.GetBytes(value);
            byte[] ArrayResultado =  cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }





      public string Decrypt(string value)
        {

            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            var BytesValue = Convert.FromBase64String(value);
            byte[] resultArray = cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //se regresa en forma de cadena
            return Encoding.UTF8.GetString(resultArray);
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