using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class BL_Categoria
    {
        private DL_Categoria oDL_Categoria = new DL_Categoria();

        public List<Categoria> ListarUsuarios()
        {
            return oDL_Categoria.ListarCategorias();
        }

        public int RegistrarCategoria(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre de la Categoría es obligatorio";
                
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Categoria.AgregarCategoria(obj, out Mensaje);

            }
            else
            {

                return 0;

            }                    
                  

        }

        public bool EditarCategoria(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre de la Categoría obligatorio";

            }
            

            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Categoria.EditarCategoria(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }

        public bool EliminarCategoria(int idCategoria, out string Mensaje)
        {
            return oDL_Categoria.EliminarCategoria(idCategoria, out Mensaje);
        }

    }
}
