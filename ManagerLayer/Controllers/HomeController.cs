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

            return Json(new { data = oUsuarios }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario oUsuario) //Recibe un objeto de tipo Usuario 
        {
            object Resultado;
            string Mensaje = string.Empty; //Variable para almacenar el mensaje de confirmación de la operación realizada (guardar o editar)

            if (oUsuario.idUsuario == 0) //Usuario nuevo 
            {

                Resultado = new BL_Usuarios().RegistrarUsuario(oUsuario, out Mensaje); //retorna el id del usuario registrado y un mensaje de confirmación 

            }
            else
            {

                Resultado = new BL_Usuarios().EditarUsuario(oUsuario, out Mensaje); //retorna true o false que significa si se edito o no el usuario  

            }

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet); //Esta línea retorna un objeto JSON con el resultado y el mensaje de la operación realizada (guardar o editar) 

        }

        [HttpPost]
        public JsonResult EliminarUsuario(int idUsuario)
        {
            object Resultado;
            string Mensaje = string.Empty;

            Resultado = new BL_Usuarios().EliminarUsuario(idUsuario, out Mensaje);

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}