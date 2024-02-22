using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BL_Marca
    {

        private DL_Marca oDL_Marca = new DL_Marca();

        public List<Marca> ListarMarcas()
        {
            return oDL_Marca.ListarMarcas();
        }

        public int RegistrarMarca(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre de la Marca es obligatorio";

            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Marca.AgregarMarca(obj, out Mensaje);

            }
            else
            {

                return 0;

            }


        }

        public bool EditarMarca(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El nombre de la Marca obligatorio";

            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return oDL_Marca.EditarMarca(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }

        public bool EliminarMarca(int idMarca, out string Mensaje)
        {
            return oDL_Marca.EliminarMarca(idMarca, out Mensaje);
        }

    }
}
