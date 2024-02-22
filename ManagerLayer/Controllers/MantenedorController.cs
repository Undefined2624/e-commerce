using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

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

        public JsonResult GetCategorias()
        {
            List<Categoria> oCategoria = new List<Categoria>();
            oCategoria = new BL_Categoria().ListarCategorias();

            return Json(new { data = oCategoria }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria oCategoria) //Recibe un objeto de tipo Usuario 
        {
            object Resultado;
            string Mensaje = string.Empty; //Variable para almacenar el mensaje de confirmación de la operación realizada (guardar o editar)

            if (oCategoria.idCategoria == 0) //Categoría nueva
            {

                Resultado = new BL_Categoria().RegistrarCategoria(oCategoria, out Mensaje); //retorna el id de la categoría registrada y un mensaje de confirmación 

            }
            else
            {

                Resultado = new BL_Categoria().EditarCategoria(oCategoria, out Mensaje); //retorna true o false que significa si se edito o no la categoría  

            }

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet); //Esta línea retorna un objeto JSON con el resultado y el mensaje de la operación realizada (guardar o editar) 

        }

        [HttpPost]
        public JsonResult EliminarCategoria(int idCategoria)
        {
            object Resultado;
            string Mensaje = string.Empty;

            Resultado = new BL_Categoria().EliminarCategoria(idCategoria, out Mensaje);

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}