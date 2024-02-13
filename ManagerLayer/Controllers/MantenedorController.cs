using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerLayer.Controllers
{
    public class MantenedorController : Controller
    {
        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Marcas()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }
    }
}