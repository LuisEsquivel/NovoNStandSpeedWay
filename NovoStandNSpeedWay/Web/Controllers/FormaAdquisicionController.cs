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
    public class FormaAdquisicionController : Controller
    {
            public ApiServices apiServices;
            public Services.Services services;
            public UsuariosController u;

            public FormaAdquisicionController()
            {
                apiServices = new ApiServices();
                services = new Services.Services();
                u = new UsuariosController();
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
                    o = services.Get<FormaAdquisicion>("formaadquisicion").Select(

                        x => new
                        {
                            x.FormaAdquisicionIdInt ,
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
                    o = services.Get<FormaAdquisicion>("formaadquisicion").Select(

                        x => new
                        {
                            x.FormaAdquisicionIdInt,
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
            public JsonResult GetByID(int id)
            {
                object o;

                try
                {
                    o = services.Get<FormaAdquisicion>("formaadquisicion").Select(

                        x => new
                        {
                            x.FormaAdquisicionIdInt ,
                            x.DescripcionVar,
                            x.ActivoBit,
                            FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                        }

                        ).Where(x => x.FormaAdquisicionIdInt  == id).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return Json(o, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            public JsonResult Add(FormaAdquisicion o, string IsActive)
            {
                o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
                FormaAdquisicion result = null;



                try
                {
                    //nuevo
                    if (o.FormaAdquisicionIdInt == 0)
                    {

                        var existe = services.Get<FormaAdquisicion>("formaadquisicion").
                                     Where(
                                     x => x.DescripcionVar == o.DescripcionVar
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
                        o.UsuarioIdInt = u.UserId();
                        result = apiServices.Save<FormaAdquisicion>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.FormaAdquisicionController, "Add", o);

                    }

                    else
                    {

                        //UPDATE
                        var existe = services.Get<FormaAdquisicion>("formaadquisicion").
                                    Where(
                                    x => x.DescripcionVar == o.DescripcionVar
                                    &&
                                    x.FormaAdquisicionIdInt   != o.FormaAdquisicionIdInt
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


                        o.UsuarioIdModInt = u.UserId();
                        result = apiServices.Save<FormaAdquisicion>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.FormaAdquisicionController, "Update", o);


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