using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataLayer;
using System.Security.Cryptography;

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
                
                string clave = BL_Recursos.GenerarClave();
                string asunto = "Registro de Usuario en Ecommerce PetsPoppins.cl";
                string mensaje = "<h3>Su cuenta en PetsPoppins.cl ha sido creada con éxito</h3><br>" +
                    "<p>Estimado(a) " + obj.nombre + " " + obj.apellidos + ", su cuenta en PetsPoppins.cl ha sido creada con éxito 😉. " +
                    "A continuación se detallan sus credenciales de acceso:</p><br>" +
                    "<p>Correo: " + obj.correo + "</p>" +
                    "<p>Clave: " + clave + "</p><br>" +
                    "<p>Para ingresar a su cuenta, haga clic en el siguiente enlace: <a href='http://localhost:44300/'>PetsPoppins.cl</a></p><br>" +
                    "<p>Atentamente, el equipo de PetsPoppins.cl 😍</p>";  

                obj.clave = BL_Recursos.convertirSHA256(clave);

                int id = oDL_Usuarios.RegistrarUsuario(obj, out Mensaje);

                if (id > 0)
                {
                    bool respuesta = BL_Recursos.EnviarCorreo(obj.correo, asunto, mensaje);
                }

                return id;
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
