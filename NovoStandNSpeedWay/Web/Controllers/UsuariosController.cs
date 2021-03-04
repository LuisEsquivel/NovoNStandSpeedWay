
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
                var usuarios = services.Get<Usuario>("usuarios");
                var roles = services.Get<Role>("roles");

                o = (from u in usuarios
                        join r in roles on u.RolIdInt equals r.RolIdInt
                        select new
                        {
                            u.UsuarioIdInt,
                            u.NombreVar,
                            r.DescripcionVar,
                            IsActiveBit = u.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(u.FechaAltaDate).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return o;
        }


        public JsonResult Listar()
        {
            object list;

            try
            {
                var usuarios = services.Get<Usuario>("usuarios");
                var roles = services.Get<Role>("roles");

                list = (from u in usuarios
                        join r in roles on u.RolIdInt equals r.RolIdInt
                        into usu
                        from us in usu.DefaultIfEmpty()
                        select new
                        {
                            u.UsuarioIdInt,
                            u.NombreVar,
                            DescripcionVar =  us?.DescripcionVar ?? "--SELECCIONE--" ,
                            IsActiveBit = u.ActivoBit != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(u.FechaAltaDate).ToShortDateString()
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
                var usuarios = services.Get<Usuario>("usuarios");
                o = (from u in usuarios
                     where u.UsuarioIdInt == id
                     select new
                     {
                         u.UsuarioIdInt,
                         u.NombreVar,
                         u.RolIdInt,
                         u.ActivoBit, 
                         u.EsAdminBit,
                         u.Password,
                         u.UsuarioVar
                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(Usuario o, string IsActive, string EsAdmin)
        {
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            o.EsAdminBit = EsAdmin != null ? Convert.ToBoolean(EsAdmin) : false;
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
                    o.UsuarioRegIdInt = hc.UserId();
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

                    o.UsuarioIdModInt = hc.UserId();
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

        #region DROPDOWN
        public JsonResult listarRoles()
        {
            object list;
            try
            {
                list = services.Get<Role>("roles").Where(p => p.ActivoBit == true).Select(
                    x => new
                    {
                        IID = x.RolIdInt,
                        NOMBRE = x.DescripcionVar
                    }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

 
   