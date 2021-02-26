using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{

        public class RolesController : Controller
        {
            public ApiServices apiServices;
            public Services.Services services;
            public HomeController hc;

            public RolesController()
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
                    o = services.Get<Role>("roles").Select(

                        x => new
                        {
                            x.RolIdInt,
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
                    o = services.Get<Role>("roles").Select(

                        x => new
                        {
                            x.UsuarioIdInt,
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
                    o = services.Get<Role>("roles").Select(

                        x => new
                        {
                            x.RolIdInt,
                            x.DescripcionVar,
                            FechaAlta = Convert.ToDateTime(x.FechaAltaDate).ToShortDateString()
                        }

                        ).Where(x => x.RolIdInt == id).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

                return Json(o, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            public JsonResult Add(Role o, string IsActive)
            {
                o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
                Role result = null;



                try
                {
                    if (o.RolIdInt  == 0)
                    {

                        var existe = services.Get<Role>("roles").
                                     Where(
                                     x => x.DescripcionVar == o.DescripcionVar
                                     ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + nameof(existe.DescripcionVar).ToString()
                                         + Environment.NewLine
                                         + "Verifique";

                            return Json(new { message });

                        }

                        // ADD
                        o.FechaAltaDate = DateTime.Now;
                        o.UsuarioIdInt = 1;
                        result = apiServices.Save<Role>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.RolesController, "Add", o);

                    }

                    else
                    {

                        //UPDATE
                        var existe = services.Get<Role>("roles").
                                    Where(
                                    x => x.DescripcionVar == o.DescripcionVar
                                    &&
                                    x.UsuarioIdInt != o.UsuarioIdInt
                                    ).FirstOrDefault();

                        if (existe != null)
                        {
                            var message = "Ya Existe un registro con estos campos: "
                                         + Environment.NewLine
                                         + nameof(existe.DescripcionVar).ToString()
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


                        o.FechaModDate = DateTime.Now;
                        o.UsuarioIdModInt = 1;
                        result = apiServices.Save<Role>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.RolesController, "Update", o);


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


 