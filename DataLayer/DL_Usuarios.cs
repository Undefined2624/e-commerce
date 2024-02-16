using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class DL_Usuarios
    {
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection osqlConnection = new SqlConnection(Connection.cn)) //clase Connection con cadena de conexión
                {
                    string query = "SELECT idUsuario, nombre, apellidos, correo, clave, restablecer, activo FROM Usuario";

                    SqlCommand cmd = new SqlCommand(query, osqlConnection);
                    cmd.CommandType = CommandType.Text;

                    osqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(
                                new Usuario()
                                {
                                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                                    nombre = reader["nombre"].ToString(),
                                    apellidos = reader["apellidos"].ToString(),
                                    correo = reader["correo"].ToString(),
                                    clave = reader["clave"].ToString(),
                                    restablecer = Convert.ToBoolean(reader["restablecer"]),
                                    activo = Convert.ToBoolean(reader["activo"])

                                });                           
                              
                        }
                    }

                }
                
            }
            catch
            {
                usuarios = new List<Usuario>();
            }

            return usuarios;
        }

    }
}
