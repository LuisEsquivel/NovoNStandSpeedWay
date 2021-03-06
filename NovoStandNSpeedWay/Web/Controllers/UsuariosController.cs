﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Auth;
using Web.Helpers;
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
        public Generals g;

        public UsuariosController()
        {
            apiServices = new ApiServices();
            services = new Services.Services();
            hc = new HomeController();
            g = new Generals();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MiPerfil()
        {
            return View();
        }


        public JsonResult ListaMiPerfil()
        {
            var MyUser = DeserializarLista<Usuario>()
                .Where(x => x.UsuarioIdInt == g.UserId() )
                .Select(
                   u => new
                   {
                       u.UsuarioIdInt,
                       u.NombreVar,
                       u.UsuarioVar,
                       u.DescripcionVar,
                       u.IsActiveBit,
                       u.FechaAlta
                   }
                ).ToList();
            return Json(MyUser, JsonRequestBehavior.AllowGet);
        }


        public List<T> DeserializarLista<T>()
        {

            List<T> lista = null;

            var result = Get();
            var json = JsonConvert.SerializeObject(result);
            lista = JsonConvert.DeserializeObject<List<T>>(json.ToString());

            return lista;

        }

  


        public object Get(bool VengoDeMiPerfil = false)
        {
            object o;

            try
            {
                var usuarios = services.Get<Usuario>("usuarios");


                if (g.IsAdmin() == false || VengoDeMiPerfil)
                {
                    usuarios = usuarios.Where(x => x.UsuarioIdInt == g.UserId() ).ToList();
                }

                var roles = services.Get<Role>("roles");

                o = (from u in usuarios
                     join r in roles on u.RolIdInt equals r.RolIdInt
                     into usu
                     from us in usu.DefaultIfEmpty()
                     select new
                     {
                         u.UsuarioIdInt,
                         u.NombreVar,
                         u.UsuarioVar,
                         DescripcionVar = us?.DescripcionVar ?? "--SELECCIONE--",
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

                var user = services.Get<Usuario>("usuarios").Where(x => x.UsuarioIdInt == g.UserId());

                var usuarios = services.Get<Usuario>("usuarios");

                if (g.IsAdmin() == false)
                {
                    usuarios = usuarios.Where(x => x.UsuarioIdInt == g.UserId()).ToList();
                }

                var roles = services.Get<Role>("roles");

                list = (from u in usuarios
                        join r in roles on u.RolIdInt equals r.RolIdInt
                        into usu
                        from us in usu.DefaultIfEmpty()
                        select new
                        {
                            u.UsuarioIdInt,
                            u.NombreVar,
                            u.UsuarioVar,
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
                         u.UsuarioVar,
                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Add(UsuarioAddOrUpdate o, string IsActive, string EsAdmin, bool VengoDeMiPerfil = false)
        {
            o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            o.EsAdminBit = EsAdmin != null ? Convert.ToBoolean(EsAdmin) : false;
            UsuarioAddOrUpdate result = null;



            try
            {
                if (o.UsuarioIdInt == 0)
                {

                    var existe = services.Get<Usuario>("usuarios").
                                 Where(
                                 x => x.UsuarioVar == o.UsuarioVar
                                 ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + "Correo Electrónico"
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    // ADD
                    o.UsuarioRegIdInt = g.UserId();
                    result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Add", o);

                }

                else
                {

                    //UPDATE
                    var existe = services.Get<UsuarioAddOrUpdate>("usuarios").
                                Where(
                                x => x.UsuarioVar == o.UsuarioVar
                                &&
                                x.UsuarioIdInt != o.UsuarioIdInt
                                ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + "Correo Electrónico"
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }



                    if (g.IsAdmin())
                    {
                        var u = services.Get<UsuarioAddOrUpdate>("usuarios").Where(x => x.UsuarioIdInt == o.UsuarioIdInt).FirstOrDefault();
                        u.RolIdInt = o.RolIdInt;
                        u.UsuarioIdModInt = g.UserId();
                        if(VengoDeMiPerfil)  u.NombreVar = o.NombreVar;
                        u.EsAdminBit = o.EsAdminBit;
                        result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Update", u);
                    }


                    if (g.IsAdmin() == false)
                    {
                        var u = services.Get<UsuarioAddOrUpdate>("usuarios").Where(x => x.UsuarioIdInt == o.UsuarioIdInt).FirstOrDefault();
                        o.RolIdInt = u.RolIdInt;
                        o.UsuarioIdModInt = g.UserId();
                        o.UsuarioVar = u.UsuarioVar;
                        result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Update", o);
                    }


                }

            }
            catch (Exception)
            {
                return null;
            }

            if (result == null) return null;


            return Json(new { message = Get(VengoDeMiPerfil) });
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

 
   