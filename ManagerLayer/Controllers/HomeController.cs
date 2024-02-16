using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using EntityLayer;

namespace ManagerLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
           return View();
        }

        public JsonResult GetUsuarios()
        {
            List<Usuario> oUsuarios = new List<Usuario>();
            oUsuarios = new BL_Usuarios().ListarUsuarios();

            return Json(new { data = oUsuarios } , JsonRequestBehavior.AllowGet);
        }   
       
    }
}