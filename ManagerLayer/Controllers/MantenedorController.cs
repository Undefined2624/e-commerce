using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;
using System.IO;

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
        #region Categorias
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

        #endregion

        #region Marcas

        public JsonResult GetMarcas()
        {
            List<Marca> oMarca = new List<Marca>();
            oMarca = new BL_Marca().ListarMarcas();

            return Json(new { data = oMarca }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca oMarca) //Recibe un objeto de tipo Usuario 
        {
            object Resultado;
            string Mensaje = string.Empty; //Variable para almacenar el mensaje de confirmación de la operación realizada (guardar o editar)

            if (oMarca.idMarca == 0) //Categoría nueva
            {

                Resultado = new BL_Marca().RegistrarMarca(oMarca, out Mensaje); //retorna el id de la categoría registrada y un mensaje de confirmación 

            }
            else
            {

                Resultado = new BL_Marca().EditarMarca(oMarca, out Mensaje); //retorna true o false que significa si se edito o no la categoría  

            }

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet); //Esta línea retorna un objeto JSON con el resultado y el mensaje de la operación realizada (guardar o editar) 

        }

        [HttpPost]
        public JsonResult EliminarMarca(int idMarca)
        {
            object Resultado;
            string Mensaje = string.Empty;

            Resultado = new BL_Marca().EliminarMarca(idMarca, out Mensaje);

            return Json(new { ResultadoJson = Resultado, MensajeJson = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Productos

        public JsonResult GetProductos()
        {
            List<Producto> oProducto = new List<Producto>();
            oProducto = new BL_Producto().ListarProductos();

            return Json(new { data = oProducto }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(string Producto, HttpPostedFileBase ArchivoImagen) //Recibe un objeto de tipo Usuario 
        {
            object Resultado;
            string Mensaje = string.Empty; //Variable para almacenar el mensaje de confirmación de la operación realizada (guardar o editar)
            bool OperacionExitosa = true;
            bool GuardarImagenExito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(Producto);

            decimal precio;

            if (decimal.TryParse(oProducto.precioTexto,
                                NumberStyles.AllowDecimalPoint, new CultureInfo("es-CL"), out precio))
            {
                oProducto.precioUnitario = precio;
            }
            else
            {
                return Json(new { OperacionExitosa = false, Mensaje = "El formato del precio debe ser 00.00" }, JsonRequestBehavior.AllowGet);
            }
                             
            
            if (oProducto.idProducto == 0) //Producto nuevo
            {
                
                int idGenerado = new BL_Producto().RegistrarProducto(oProducto, out Mensaje);
                
                if (idGenerado != 0)
                {

                    oProducto.idProducto = idGenerado;

                }
                else
                {

                    OperacionExitosa = false;

                }               

            }
            else
            {

                OperacionExitosa = new BL_Producto().EditarProducto(oProducto, out Mensaje); //retorna true o false que significa si se edito o no la categoría  

            }

            if (OperacionExitosa)
            {
                if (ArchivoImagen != null)
                {
                    
                    string rutaAlmacenamiento = ConfigurationManager.AppSettings["AlmacenamientoImagenes"];
                    string extension = Path.GetExtension(ArchivoImagen.FileName);
                    string nombreImagen = string.Concat(oProducto.idProducto.ToString(), extension); //Se concatena el id del producto con la extensión del archivo para que no se repitan nombres de archivos
                
                    try
                    {

                        ArchivoImagen.SaveAs(Path.Combine(rutaAlmacenamiento, nombreImagen)); //Se guarda la imagen en la ruta especificada en el archivo de configuración

                    }catch(Exception e)
                    {
                        
                        string mns = e.Message;
                        GuardarImagenExito = false;

                    }

                    if (GuardarImagenExito)
                    {
                        oProducto.rutaImagen = rutaAlmacenamiento;
                        oProducto.nombreImagen = nombreImagen;
                        bool rspta = new BL_Producto().GuardarDatosImagen(oProducto, out Mensaje);

                    }else
                    {

                        Mensaje = "Se guardó el producto, pero hubo problemas con la imagen";

                    }


                }
            }

            return Json(new { OperacionExitosa = OperacionExitosa, idGenerado = oProducto.idProducto, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}