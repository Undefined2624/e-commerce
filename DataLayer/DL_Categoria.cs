using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DL_Categoria
    {

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (SqlConnection osqlConnection = new SqlConnection(Connection.cn)) //clase Connection con cadena de conexión
                {
                    string query = "SELECT idCategoria, nombre, descripcion, activo FROM Categoria";

                    SqlCommand cmd = new SqlCommand(query, osqlConnection);
                    cmd.CommandType = CommandType.Text;

                    osqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(
                                new Categoria()
                                {
                                    idCategoria = Convert.ToInt32(reader["idCategoria"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    activo = Convert.ToBoolean(reader["activo"])

                                });

                        }
                    }

                }

            }
            catch
            {
                categorias = new List<Categoria>();
            }

            return categorias;
        }

        public int AgregarCategoria(Categoria obj, out string Mensaje)
        {
            int idAutogerenado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_AgregarCategoria", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure; // Especifica que el comando es un procedimiento almacenado
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutogerenado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception e)
            {
                idAutogerenado = 0;
                Mensaje = e.Message;
            }

            return idAutogerenado;

        }

        public bool EditarCategoria(Categoria obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oconexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", obj.idCategoria);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

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

        public bool EliminarCategoria(int idCategoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oconexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

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
