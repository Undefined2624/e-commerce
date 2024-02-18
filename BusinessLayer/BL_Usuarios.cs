using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataLayer;

namespace BusinessLayer
{
    public class BL_Usuarios
    {
        private DL_Usuarios oDL_Usuarios = new DL_Usuarios();

        public List<Usuario> ListarUsuarios()
        {
            return oDL_Usuarios.ListarUsuarios();
        }

        public int RegistrarUsuario(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre)) { 

                  Mensaje = "El campo nombre es obligatorio";
                  
            }else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                  Mensaje = "El campo apellidos es obligatorio";
                 
            }else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                  Mensaje = "El campo correo es obligatorio";
               
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = "test123";
                obj.clave = BL_Recursos.convertirSHA256(clave);

                return oDL_Usuarios.RegistrarUsuario(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
                       
        }

        public bool EditarUsuario(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                Mensaje = "El campo nombre es obligatorio";

            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                Mensaje = "El campo apellidos es obligatorio";

            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "El campo correo es obligatorio";

            }

            if (string.IsNullOrEmpty(Mensaje))
            {

              return oDL_Usuarios.EditarUsuario(obj, out Mensaje);

            }
            else
            {
                return false;
            }
           
        }

        public bool EliminarUsuario(int idUsuario, out string Mensaje)
        {
            return oDL_Usuarios.EliminarUsuario(idUsuario, out Mensaje);
        }

    }
}
