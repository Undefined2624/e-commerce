using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.SqlClient;
using System.Data;
using System.Net.Configuration;
using System.CodeDom;

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

        public int registrarUsuario(Usuario obj, out string Mensaje)
        {
            int idAutogerenado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                     SqlCommand cmd = new SqlCommand("sp_registrarUsuario", oconexion);
                     cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                     cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                     cmd.Parameters.AddWithValue("@correo", obj.correo);
                     cmd.Parameters.AddWithValue("@clave", obj.clave);
                     cmd.Parameters.AddWithValue("@activo", obj.activo);   
                     cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                     oconexion.Open();

                     cmd.ExecuteNonQuery();

                     idAutogerenado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                     Mensaje = cmd.Parameters["mensaje"].Value.ToString();                    

                }
            }   
            catch(Exception e)
            {
                idAutogerenado = 0;
                Mensaje = e.Message;
            }

            return idAutogerenado;

        }

        public bool editarUsuario(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", obj.idUsuario);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception e)
            {
                resultado = false;
                Mensaje = e.Message;
            }

            return resultado;

        }   

    }
}
