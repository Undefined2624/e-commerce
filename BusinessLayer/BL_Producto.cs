using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BL_Producto
    {

        DL_Producto oDL_Producto = new DL_Producto();

        public List<Producto> ListarProductos()
        {
            return oDL_Producto.ListarProductos();
        }

        public int RegistrarProducto(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre del producto es obligatorio";

            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Producto.AgregarProducto(obj, out Mensaje);

            }
            else
            {

                return 0;

            }


        }

        public bool EditarProducto(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre del producto obligatorio";

            }
            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {

                Mensaje = "La descripción del producto es obligatoria";

            }
            else if (obj.Marca.idMarca == 0)
            {

                Mensaje = "La marca del producto es obligatoria";

            }
            else if (obj.Categoria.idCategoria == 0)
            {

                Mensaje = "La categoría del producto es obligatoria";

            }
            else if (obj.precioUnitario == 0)
            {
                Mensaje = "El precio del producto es obligatorio";
            }
            else if (obj.stock == 0)
            {
                Mensaje = "El stock es obligatorio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Producto.EditarProducto(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }

        public bool EliminarProducto(int idProducto, out string Mensaje)
        {
            return oDL_Producto.EliminarProducto(idProducto, out Mensaje);
        }

        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            return oDL_Producto.GuardarDatosImagen(obj, out Mensaje);
        }


    }
}
